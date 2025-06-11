using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class ConsoleViewer : EditorWindow
{
    private Vector2 scrollPosition;
    private static List<LogEntry> logs = new List<LogEntry>();
    private bool autoScroll = true;
    private string filterText = "";
    private bool showErrors = true;
    private bool showWarnings = true;
    private bool showMessages = true;

    [MenuItem("Window/Console Viewer")]
    public static void ShowWindow()
    {
        GetWindow<ConsoleViewer>("Console Viewer");
    }

    void OnEnable()
    {
        // Подписываемся на события логов
        Application.logMessageReceived += HandleLog;
        EditorApplication.playModeStateChanged += HandlePlayModeChange;
    }

    void OnDisable()
    {
        // Отписываемся от событий
        Application.logMessageReceived -= HandleLog;
        EditorApplication.playModeStateChanged -= HandlePlayModeChange;
    }

    private void HandlePlayModeChange(PlayModeStateChange state)
    {
        // Очищаем логи при переключении режима
        if (state == PlayModeStateChange.ExitingPlayMode || 
            state == PlayModeStateChange.EnteredEditMode)
        {
            logs.Clear();
            Repaint();
        }
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Добавляем новую запись
        logs.Add(new LogEntry
        {
            message = logString,
            stackTrace = stackTrace,
            type = type,
            timestamp = System.DateTime.Now.ToString("HH:mm:ss")
        });
        
        // Обновляем окно
        Repaint();
    }

    void OnGUI()
    {
        DrawToolbar();
        DrawLogList();
    }

    void DrawToolbar()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        {
            // Фильтр по тексту
            filterText = EditorGUILayout.TextField(filterText, EditorStyles.toolbarSearchField, GUILayout.Width(200));
            
            // Фильтры по типу
            showErrors = GUILayout.Toggle(showErrors, "Errors", EditorStyles.toolbarButton);
            showWarnings = GUILayout.Toggle(showWarnings, "Warnings", EditorStyles.toolbarButton);
            showMessages = GUILayout.Toggle(showMessages, "Messages", EditorStyles.toolbarButton);
            
            GUILayout.FlexibleSpace();
            
            // Очистка логов
            if (GUILayout.Button("Clear", EditorStyles.toolbarButton))
            {
                logs.Clear();
            }
            
            // Автопрокрутка
            autoScroll = GUILayout.Toggle(autoScroll, "Auto Scroll", EditorStyles.toolbarButton);
        }
        EditorGUILayout.EndHorizontal();
    }

    void DrawLogList()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        {
            int count = 0;
            for (int i = logs.Count - 1; i >= 0; i--)
            {
                var log = logs[i];
                
                // Применяем фильтры
                if (!PassesFilters(log)) continue;
                
                // Определяем стиль в зависимости от типа
                GUIStyle style = GetLogStyle(log.type);
                
                // Отображаем запись
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label(log.timestamp, GUILayout.Width(60));
                    GUILayout.Label(log.message, style);
                }
                EditorGUILayout.EndHorizontal();
                
                count++;
                
                // Ограничиваем количество видимых записей для производительности
                if (count > 500) break;
            }
            
            // Автопрокрутка вниз
            if (autoScroll && Event.current.type == EventType.Repaint)
            {
                scrollPosition.y = float.MaxValue;
            }
        }
        EditorGUILayout.EndScrollView();
    }

    bool PassesFilters(LogEntry log)
    {
        // Фильтр по типу
        if (log.type == LogType.Error && !showErrors) return false;
        if (log.type == LogType.Warning && !showWarnings) return false;
        if (log.type == LogType.Log && !showMessages) return false;
        
        // Фильтр по тексту
        if (!string.IsNullOrEmpty(filterText))
        {
            if (!log.message.Contains(filterText) && 
                !log.stackTrace.Contains(filterText))
            {
                return false;
            }
        }
        
        return true;
    }

    GUIStyle GetLogStyle(LogType type)
    {
        GUIStyle style = new GUIStyle(EditorStyles.label)
        {
            wordWrap = true,
            richText = true
        };
        
        // Цвета для разных типов логов
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                style.normal.textColor = Color.red;
                return style;
                
            case LogType.Warning:
                style.normal.textColor = Color.yellow;
                return style;
                
            case LogType.Log:
            default:
                return EditorStyles.label;
        }
    }

    // Структура для хранения логов
    private struct LogEntry
    {
        public string message;
        public string stackTrace;
        public LogType type;
        public string timestamp;
    }
}