using System;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using WhiteZhi.SimulationGame;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ForDebug
{
    public class ForDebugBehaviour : MonoBehaviour
    {
        public bool focusOnStartGame;
        private void Start()
        {
            DontDestroyOnLoad(this);
#if UNITY_EDITOR
            if (focusOnStartGame)
            {
                Selection.activeGameObject = this.gameObject;
            }
#endif
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution( 640,360);
            GUILayout.Space( 10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label($"天数:{Global.Days.Value}");
            GUILayout.EndHorizontal();
        }

        [Button("天数+1",ButtonSizes.Small)]
        public void AddDays()
        {
            Global.Days.Value++;
        }
        [Button("天数-1",ButtonSizes.Small)]
        public void SubDays()
        {
            Global.Days.Value--;
        }
        
    }
}