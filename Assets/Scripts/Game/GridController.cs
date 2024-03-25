using System;
using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace WhiteZhi.SimulationGame
{
	public partial class GridController : ViewController
	{
		
		public EasyGrid<SoilData> digGrid = new EasyGrid<SoilData>(50,50);
		public TileBase digTile;
		
		private void Awake()
		{
			
		}

		public void LoadTilemap()
		{
			for (int i = 5; i <= 10; i++)
			{
				for (int j = 1; j <= 5; j++)
				{
					digGrid[i, j] = new SoilData();
					digTilemap.SetTile(new Vector3Int(i, j, 0),digTile);
				}
			}
		}
	}
}
