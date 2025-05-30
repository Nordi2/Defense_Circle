using System;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class GameInfoWindow :EditorWindow
    {
        [MenuItem("Tools/Game Info")]
        public static void ShowWindow()
        {
            GetWindow<GameInfoWindow>("Game Info");
        }

        private void OnGUI()
        {
            GUILayout.Label("Game Info:");
            
            GUILayout.Label("Count Spawn Enemy");
            
        }
    }
}