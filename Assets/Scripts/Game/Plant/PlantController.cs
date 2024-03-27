using UnityEngine;
using QFramework;

namespace WhiteZhi.SimulationGame
{
	public enum PlantStates
	{
		Seed,
		Grow,
		Harvest
	}
	public partial class PlantController : ViewController ,ISingleton
	{
		public static PlantController Instance
		{
			get => MonoSingletonProperty<PlantController>.Instance;
		}

		public EasyGrid<PlantBase> plants = new EasyGrid<PlantBase>(50, 50);

		public void OnSingletonInit()
		{
			
		}
	}
}
