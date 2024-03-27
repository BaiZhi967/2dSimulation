using System.Collections.Generic;
using UnityEngine;

namespace WhiteZhi.SimulationGame
{
    [CreateAssetMenu(menuName = "游戏数据/所有植物数据列表")]
    public class PlantDataListSO : ScriptableObject
    {
        public List<PlantData> plantDatas;
    }

}