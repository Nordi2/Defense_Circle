using System;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class GameInfoWindow : EditorWindow
    {
        [MenuItem("Tools/Game Info")]
        public static void ShowWindow()
        {
            GetWindow<GameInfoWindow>("Game Info");
        }

        private void OnGUI()
        {
            GUILayout.Label("Game Info:");

            GUILayout.Label("Count Spawn Enemy", EditorStyles.linkLabel);

            GUIStyle style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("node4") as Texture2D;

            if (GUILayout.Button("click", style, GUILayout.Width(100), GUILayout.Height(50)))
            {
                Debug.Log("+");
            }
        }
    }
}