using UnityEngine;
using QFramework;


namespace WhiteZhi.SimulationGame
{
	
	public partial class PlantBase : ViewController
	{
		public PlantData plantData;
		public SoilData soilData;
		public Vector2Int cell;
		
		private PlantStates mPlantState = PlantStates.Seed;

		public PlantStates State => mPlantState;

		public void SetState(PlantStates newState)
		{
			if (newState != mPlantState)
			{
				mPlantState = newState;
				//切换表现
				
				//同步到 digData
				FindObjectOfType<GridController>().digGrid[cell.x, cell.y].plantState = mPlantState;
			}
		}

	}
}
