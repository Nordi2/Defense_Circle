using UnityEditor;
using UnityEngine;

public class GameViewWindow : EditorWindow
{
    private RenderTexture renderTexture;
    private Camera gameCamera;
    private Vector2 scrollPosition;

    [MenuItem("Window/Game Preview")]
    public static void ShowWindow()
    {
        GetWindow<GameViewWindow>("Game Preview");
    }
    
    void OnEnable()
    {
        // Создаем Render Texture
        renderTexture = new RenderTexture(1920, 1080, 24);
        renderTexture.Create();
        
        // Находим главную камеру
        FindGameCamera();
        
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    void OnDisable()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
            DestroyImmediate(renderTexture);
        }
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            FindGameCamera();
        }
    }

    void FindGameCamera()
    {
        if (Application.isPlaying)
        {
            gameCamera = Camera.main;
            if (gameCamera != null)
            {
                gameCamera.targetTexture = renderTexture;
            }
        }
    }

    void Update()
    {
        // Постоянно обновляем окно для плавного отображения
        if (gameCamera != null && Application.isPlaying)
        {
            Repaint();
        }
    }

    void OnGUI()
    {
        if (gameCamera == null)
        {
            EditorGUILayout.HelpBox("Game camera not found. Enter Play Mode and ensure there's a camera tagged 'MainCamera'.", MessageType.Info);
            
            if (GUILayout.Button("Refresh Camera"))
            {
                FindGameCamera();
            }
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        {
            // Рассчитываем соотношение сторон
            float aspect = (float)renderTexture.width / renderTexture.height;
            float width = Mathf.Min(position.width - 20, renderTexture.width);
            float height = width / aspect;
            
            // Отображаем текстуру
            Rect textureRect = GUILayoutUtility.GetRect(width, height);
            EditorGUI.DrawPreviewTexture(textureRect, renderTexture);
            
            // Отображаем информацию о камере
            EditorGUILayout.LabelField($"Camera: {gameCamera.name}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Resolution: {renderTexture.width}x{renderTexture.height}");
        }
        EditorGUILayout.EndScrollView();
    }
}