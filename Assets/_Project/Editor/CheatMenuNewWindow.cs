using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.EnemyLogic;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class CheatMenuNewWindow : EditorWindow
    {
        private bool _showMoneySection;
        private bool _showHealthSection;
        private bool _showEnemySection;

        private GUIStyle _initialStyle;
        private GUIStyle _headerStyle;
        private GUIStyle _foldoutStyle;
        private Texture2D _backgroundTexture;

        private int _addMoney;
        private int _spendMoney;
        private int _addHealth;
        private int _spendHealth;

        private bool _gameStarted => EditorApplication.isPlaying;

        [MenuItem("Tools/Cheat-Menu")]
        public static void ShowWindow()
        {
            GetWindow<CheatMenuNewWindow>("Cheat Menu");
        }

        private void OnEnable()
        {
            _backgroundTexture = MakeText(2, 2, new Color(1, 0.61f, 0));

            _initialStyle = new GUIStyle()
            {
                normal = { textColor = new Color(1, 0.61f, 0) },
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
            };

            _foldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                normal =
                {
                    textColor = new Color(1, 0.61f, 0)
                },
                fontSize = 14,
                alignment = TextAnchor.MiddleLeft,
            };

            _headerStyle = new GUIStyle(EditorStyles.helpBox)
            {
                normal =
                {
                    background = _backgroundTexture,
                    textColor = Color.black
                },
                padding = new RectOffset(10, 10, 5, 5),
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
            };
        }

        private void OnDisable()
        {
            if (_backgroundTexture == null)
                return;

            DestroyImmediate(_backgroundTexture);
            _backgroundTexture = null;
        }

        private void OnGUI()
        {
            EditorGUI.DrawRect(new Rect(0, 0, position.width, position.height), Color.black);

            if (!_gameStarted)
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label("Start the game for the menu to appear", _initialStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                ResettingFieldValueAndFindCheatManager();
                return;
            }

            Rect horizontalGroup = EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                EditorGUI.DrawRect(horizontalGroup, new Color(0.9f, 0.5f, 0.2f));

                Rect verticalGroupCheatMenu = EditorGUILayout.BeginVertical("window", GUILayout.Width(200));
                {
                    EditorGUI.DrawRect(verticalGroupCheatMenu, Color.black);
                    GUILayout.Label("Cheat-Menu", _headerStyle, GUILayout.ExpandWidth(true));

                    GUILayout.Space(10);

                    #region Money Section

                    _showMoneySection =
                        EditorGUILayout.BeginFoldoutHeaderGroup(_showMoneySection, "Money Section", _foldoutStyle);
                    EditorGUILayout.EndFoldoutHeaderGroup();

                    if (_showMoneySection)
                    {
                        EditorGUILayout.BeginVertical("box");
                        {
                            GUILayout.BeginHorizontal(EditorStyles.toolbar);
                            {
                                if (GUILayout.Button(
                                        new GUIContent("", EditorGUIUtility.IconContent("d_Toolbar Plus").image),
                                        EditorStyles.toolbarButton,
                                        GUILayout.Height(30)))
                                {
                                    AddMoney();
                                }

                                _addMoney = EditorGUILayout.IntField(_addMoney, EditorStyles.toolbarTextField,
                                    GUILayout.Height(30));
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal(EditorStyles.toolbar);
                            {
                                if (GUILayout.Button(
                                        new GUIContent("", EditorGUIUtility.IconContent("d_Toolbar Minus").image),
                                        EditorStyles.toolbarButton,
                                        GUILayout.Height(30)))
                                {
                                    SpendMoney();
                                }

                                _spendMoney = EditorGUILayout.IntField(_spendMoney, EditorStyles.toolbarTextField,
                                    GUILayout.Height(30));
                                GUILayout.EndHorizontal();
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }

                    #endregion

                    #region Health Section

                    _showHealthSection =
                        EditorGUILayout.BeginFoldoutHeaderGroup(_showHealthSection, "Health Section", _foldoutStyle);
                    EditorGUILayout.EndFoldoutHeaderGroup();

                    if (_showHealthSection)
                    {
                        EditorGUILayout.BeginVertical("box");
                        {
                            GUILayout.BeginHorizontal(EditorStyles.toolbar);
                            {
                                if (GUILayout.Button(
                                        new GUIContent("", EditorGUIUtility.IconContent("d_Toolbar Plus").image),
                                        EditorStyles.toolbarButton,
                                        GUILayout.Height(30)))
                                    AddHealthTower();

                                _addHealth = EditorGUILayout.IntField(_addHealth, EditorStyles.toolbarTextField,
                                    GUILayout.Height(30));
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal(EditorStyles.toolbar);
                            {
                                if (GUILayout.Button(
                                        new GUIContent("", EditorGUIUtility.IconContent("d_Toolbar Minus").image),
                                        EditorStyles.toolbarButton,
                                        GUILayout.Height(30)))
                                    LowerHealth();

                                _spendHealth = EditorGUILayout.IntField(_spendHealth, EditorStyles.toolbarTextField,
                                    GUILayout.Height(30));
                                GUILayout.EndHorizontal();
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }

                    #endregion

                    #region EnemySection

                    _showEnemySection =
                        EditorGUILayout.BeginFoldoutHeaderGroup(_showEnemySection, "Enemy Section", _foldoutStyle);
                    EditorGUILayout.EndFoldoutHeaderGroup();

                    if (_showEnemySection)
                    {
                        EditorGUILayout.BeginVertical("box");
                        {
                            if (GUILayout.Button("Create Fast Enemy", EditorStyles.miniButton))
                                CreateEnemy(EnemyType.Fast);
                            if (GUILayout.Button("Create Default Enemy", EditorStyles.miniButton))
                                CreateEnemy(EnemyType.Default);
                            if (GUILayout.Button("Create Slow Enemy", EditorStyles.miniButton))
                                CreateEnemy(EnemyType.Slow);

                            EditorGUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Kill Random Enemy", EditorStyles.miniButton))
                                    KillRandomEnemy();

                                if (GUILayout.Button("Kill All Enemy", EditorStyles.miniButton))
                                    KillAllEnemy();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();
                    }

                    #endregion
                }
                EditorGUILayout.EndVertical();


                GUILayout.Space(5);

                Rect verticalGroupGameInfo =
                    EditorGUILayout.BeginVertical(GUI.skin.window, GUILayout.ExpandWidth(true));
                {
                    EditorGUI.DrawRect(verticalGroupGameInfo, Color.black);
                    GUILayout.Label("Game Info", _headerStyle, GUILayout.ExpandWidth(true));
                }
                EditorGUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            /*GUILayout.Label("Menu Cheats:", EditorStyles.boldLabel);

            GUILayout.Space(10);

            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(400));

            GUILayout.Label("Money Section:");
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Money", GUILayout.MaxWidth(150)))
                AddMoney();

            _addMoney = EditorGUILayout.IntField("", _addMoney, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Spend Money", GUILayout.MaxWidth(150)))
                SpendMoney();

            _spendMoney = EditorGUILayout.IntField("", _spendMoney, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Health Section:");

            EditorGUILayout.BeginHorizontal(GUILayout.Width(200));
            if (GUILayout.Button("Restore Health", GUILayout.MaxWidth(150)))
            {
                AddHealthTower();
            }

            _addHealth = EditorGUILayout.IntField("", _addHealth, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.Width(200));

            if (GUILayout.Button("Lower Health", GUILayout.MaxWidth(150)))
                LowerHealth();

            _spendHealth = EditorGUILayout.IntField("", _spendHealth, GUILayout.MaxWidth(50));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Another:");

            if (GUILayout.Button("Create Fast Enemy", GUILayout.MaxWidth(150)))
                CreateEnemy(EnemyType.Fast);

            if (GUILayout.Button("Create Default Enemy", GUILayout.MaxWidth(150)))
                CreateEnemy(EnemyType.Default);

            if (GUILayout.Button("Create Slow Enemy", GUILayout.MaxWidth(150)))
                CreateEnemy(EnemyType.Slow);

            if (GUILayout.Button("Kill Random Enemy", GUILayout.MaxWidth(150)))
                KillRandomEnemy();

            if (GUILayout.Button("Kill All Enemy", GUILayout.MaxWidth(150)))
                KillAllEnemy();

            EditorGUILayout.EndVertical();*/
        }

        private Texture2D MakeText(int width, int height, Color color)
        {
            Color[] pixel = new Color[width * height];

            for (int i = 0; i < pixel.Length; i++)
                pixel[i] = color;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pixel);
            result.Apply();
            return result;
        }

        private void ResettingFieldValueAndFindCheatManager()
        {
            Repaint();

            _addMoney = 0;
            _spendMoney = 0;
            _addHealth = 0;
            _spendHealth = 0;
        }

        private void KillAllEnemy() =>
            CheatManager.KillAllSpawnedEnemies();

        private void KillRandomEnemy() =>
            CheatManager.KillRandomSpawnedEnemies();

        private void CreateEnemy(EnemyType type) =>
            CheatManager.SpawnEnemy(type);

        private void LowerHealth() =>
            CheatManager.TakeDamage(_spendHealth);

        private void SpendMoney() =>
            CheatManager.SpendMoney(_spendMoney);

        private void AddHealthTower()
        {
        }

        private void AddMoney() =>
            CheatManager.AddMoney(_addMoney);
    }
}