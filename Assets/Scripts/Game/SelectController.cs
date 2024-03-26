using UnityEngine;
using QFramework;

namespace WhiteZhi.SimulationGame
{
	public partial class SelectController : ViewController , ISingleton
	{
		public static SelectController Instance
		{
			get => MonoSingletonProperty<SelectController>.Instance;
		}

		public void OnSingletonInit()
		{
			
		}
	}
}
