using UnityEngine;
using QFramework;

namespace WhiteZhi.SimulationGame
{
	public partial class ResController : ViewController,ISingleton
	{
		/// <summary>
		/// 单例
		/// </summary>
		public static ResController Instance
		{
			get => MonoSingletonProperty<ResController>.Instance;
		}
		public GameObject plantPrefab;
		void Start()
		{
			// Code Here
		}

		public void OnSingletonInit()
		{
			
		}
	}
}
