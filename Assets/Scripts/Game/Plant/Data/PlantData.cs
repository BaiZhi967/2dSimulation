﻿using UnityEngine;

namespace WhiteZhi.SimulationGame
{
    [System.Serializable]
    public class PlantData
    {
        public int seedItemId;
        [Header("不同阶段需要的天数")]
        public int[] growthDays;
        public int TotalGrowthDays
        {
            get
            {
                int amount = 0;
                foreach (var days in growthDays)
                {
                    amount += days;
                }
                return amount;
            }
        }
        [Header("不同生长阶段物品Prefab")]
        public GameObject[] growthPrefabs;
        [Header("不同阶段的图片")]
        public Sprite[] growthSprites;

        [Space]
        [Header("收割工具")]
        public int[] harvestToolItemID;
        [Header("每种工具使用次数")]
        public int[] requireActionCount;
        [Header("转换新物品ID")]
        public int transferItemID;

        [Space]
        [Header("收割果实信息")]
        public int[] producedItemID;
        public int[] producedMinAmount;
        public int[] producedMaxAmount;
        public Vector2 spawnRadius;

        [Space]
        [Header("再次生长时间")]
        public int daysToRegrow;
        [Header("再次生长次数")]
        public int regrowTimes;

        [Header("Options")]
        public bool generateAtPlayerPosition;
        public bool hasAnimation;
        public bool hasParticleEffect;
        

        public Vector3 effectPos;



        /// <summary>
        /// 检测工具是否可用
        /// </summary>
        /// <param name="toolID">工具id</param>
        /// <returns>是否可用</returns>
        public bool CheckToolAvailable(int toolID)
        {
            foreach (var id in harvestToolItemID)
            {
                if (id == toolID) return true;
            }

            return false;
        }

        public int GetTotalRequireCount(int toolID)
        {
            for (int i = 0; i < harvestToolItemID.Length; i++)
            {
                if (harvestToolItemID[i] == toolID)
                {
                    return requireActionCount[i];
                }
            }

            return -1;
        }
    }
}