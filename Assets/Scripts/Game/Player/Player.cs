using System;
using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace WhiteZhi.SimulationGame
{
	public partial class Player : ViewController
	{
		public PlayerInput playerInput;

		public float walkSpeed;
		public Vector2 inputDirection;
		public Vector2 realDirection;

		private void Awake()
		{
			playerInput = new PlayerInput();
		}

		private void Update()
		{
			//获取玩家移动输入
			inputDirection = playerInput.Game.Move.ReadValue<Vector2>();
		}

		private void FixedUpdate()
		{
			Movement();//移动
		}

		private void OnEnable()
		{
			playerInput.Enable();
			
		}

		private void OnDisable()
		{
			playerInput.Disable();
		}

		
		
		/// <summary>
		/// 玩家移动逻辑的处理
		/// </summary>
		private void Movement()
		{
			//如果没有输入 则直接返回
			if (inputDirection.Equals(Vector2.zero))
			{
				return;
			}
			realDirection = inputDirection;
			if (inputDirection.x != 0 && inputDirection.y != 0)
			{
				realDirection = new Vector2(inputDirection.x * 0.7f, inputDirection.y * 0.7f);
			}

			//玩家方向修改
			if (realDirection.x > 0)
			{
				playerTransform.localScale = new Vector3(-1, 1, 1);
			}
			else if (realDirection.x < 0)
			{
				playerTransform.localScale = new Vector3(1, 1, 1);
			}
			rigidbody.MovePosition(rigidbody.position + realDirection * walkSpeed * Time.deltaTime);
		}
	}
}
