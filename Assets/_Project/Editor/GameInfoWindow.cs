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

        private Color buttonColor = Color.green;

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
            
            GUIStyle iconButtonStyle = new GUIStyle(GUI.skin.button)
            {
                imagePosition = ImagePosition.ImageAbove,
                fixedWidth = 60,
                fixedHeight = 60
            };

            if (GUILayout.Button(new GUIContent("Play", EditorGUIUtility.IconContent("PlayButton").image),
                    iconButtonStyle))
            {
                // Запуск процесса
            }

            EditorGUILayout.LabelField("Standard Label", EditorStyles.boldLabel);

            if (GUILayout.Button("Mini Button", EditorStyles.miniButton))
            {
                // Обработка клика
            }

            EditorGUILayout.HelpBox("This is information", MessageType.Info);

            GUIStyle errorStyle = new GUIStyle(EditorStyles.textField)
            {
                normal = { textColor = Color.red },
                fontStyle = FontStyle.Italic
            };

            EditorGUILayout.TextField("Error Field", "Invalid value", errorStyle);

            GUIStyle coloredButtonStyle = new GUIStyle(GUI.skin.button)
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.white,
                    background = MakeTex(2, 2, buttonColor)
                },
                hover = new GUIStyleState()
                {
                    textColor = Color.yellow,
                    background = MakeTex(2, 2, Color.Lerp(buttonColor, Color.white, 1))
                },
                active = new GUIStyleState()
                {
                    textColor = Color.black,
                    background = MakeTex(2, 2, Color.Lerp(buttonColor, Color.black, 1))
                },
                fixedHeight = 30,
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
            };

            GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                normal = { textColor = new Color(0.1f, 0.5f, 0.8f) },
                padding = new RectOffset(5, 5, 10, 10)
            };

            EditorGUILayout.LabelField("Section Header", headerStyle);

            if (GUILayout.Button("Colorful Button", coloredButtonStyle, GUILayout.Width(200)))
            {
                Debug.Log("Color button clicked");
            }

            buttonColor = EditorGUILayout.ColorField("Button Color", buttonColor);
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}