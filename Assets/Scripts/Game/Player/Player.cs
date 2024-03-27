using System;
using UnityEngine;
using QFramework;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

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
		
		public GridController gc;
                       
		private void Awake()
		{
			playerInput = new PlayerInput();

			Global.Days.Register(i =>
			{
				
				
				PlantController.Instance.plants.ForEach((x, y, plant) =>
				{
					if (plant is not null)
					{
						if (plant.State == PlantStates.Seed)
						{
							if (gc.digGrid[x,y].watered)
							{
								//植物生长
							}
						}
					}
				});
				
			});
		}

		private void Start()
		{
			gc = FindObjectOfType<GridController>();
		}

		private void Update()
		{
			//获取玩家移动输入
			inputDirection = playerInput.Game.Move.ReadValue<Vector2>();

			//TileSelect位置更新
			UpdateTileSelectPos();
		}


		private void FixedUpdate()
		{
			Movement();//移动
		}

		private void OnEnable()
		{ 
			playerInput.Enable();
			playerInput.Game.Use.started += OnPlayerUseTool;
			playerInput.Game.RightMouse.started += OnPlayerRightMouse;
			playerInput.Game.Interactive.started += OnPlayerInteractive;
		}

		private void OnDisable()
		{
			playerInput.Disable();
			playerInput.Game.Use.started -= OnPlayerUseTool;
			playerInput.Game.RightMouse.started -= OnPlayerRightMouse;
			playerInput.Game.Interactive.started -= OnPlayerInteractive;
		}

		///<summary>
		/// 玩家使用工具 （左键）
		/// </summary>
		private void OnPlayerUseTool(InputAction.CallbackContext obj)
		{
			var cellPosition = gc.grid.WorldToCell(transform.position);
			var tile = gc.digTilemap.GetTile(cellPosition);
			
			if (cellPosition.x >= 0 && cellPosition.x < 50 && cellPosition.y >= 0 && cellPosition.y <=50)
			{
				if (gc.digGrid[cellPosition.x,cellPosition.y] == null)
				{
					//耕地
					gc.digTilemap.SetTile(cellPosition,gc.digTile);
					gc.digGrid[cellPosition.x,cellPosition.y] = new SoilData();
				}
				else if (!gc.digGrid[cellPosition.x,cellPosition.y].hasSeed)
				{
					//种植 放种子
					var pos = gc.grid.CellToWorld(cellPosition);
					pos.x += gc.grid.cellSize.x * 0.5f;
					pos.y += gc.grid.cellSize.y * 0.5f;
					var plant = ResController.Instance.plantPrefab.Instantiate().Position(pos);
					var pb = plant.GetComponent<PlantBase>();
					pb.cell = new Vector2Int(cellPosition.x, cellPosition.y);
					PlantController.Instance.plants[cellPosition.x, cellPosition.y] = plant.GetComponent<PlantBase>();
					gc.digGrid[cellPosition.x, cellPosition.y].hasSeed = true;
				}
				
			}
			
		}

		private void OnPlayerRightMouse(InputAction.CallbackContext obj)
		{
			var cellPosition = gc.grid.WorldToCell(transform.position);
			var tile = gc.digTilemap.GetTile(cellPosition);
			if (cellPosition.x >= 0 && cellPosition.x < 50 && cellPosition.y >= 0 && cellPosition.y <=50)
			{
				if (gc.digGrid[cellPosition.x,cellPosition.y] != null)
				{
					gc.digTilemap.SetTile(cellPosition,null);
					gc.digGrid[cellPosition.x, cellPosition.y] = null;
				}
				
			}
		}

		private void OnPlayerInteractive(InputAction.CallbackContext obj)
		{
			var cellPosition = gc.grid.WorldToCell(transform.position);
			var tile = gc.waterTilemap.GetTile(cellPosition);
			if (cellPosition.x >= 0 && cellPosition.x < 50 && cellPosition.y >= 0 && cellPosition.y <=50)
			{
				if (gc.digGrid[cellPosition.x,cellPosition.y] != null)
				{
					if (!gc.digGrid[cellPosition.x,cellPosition.y].watered)
					{
						gc.waterTilemap.SetTile(cellPosition,gc.waterTile);
						gc.digGrid[cellPosition.x, cellPosition.y].watered = true;
					}
				}
				
			}
		}

		/// <summary>
		/// 更新TileSelect位置
		/// </summary>
		private void UpdateTileSelectPos()
		{
			var cellPosition = gc.grid.WorldToCell(transform.position);
			var pos = gc.grid.CellToWorld(cellPosition);
			pos.x += gc.grid.cellSize.x * 0.5f;
			pos.y += gc.grid.cellSize.y * 0.5f;
			if (cellPosition.x >= 0 && cellPosition.x < 50 && cellPosition.y >= 0 && cellPosition.y <=50)
			{
				SelectController.Instance.Position(pos);
				SelectController.Instance.Show();
			}
			else
			{
				SelectController.Instance.Hide();
			}
			
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
