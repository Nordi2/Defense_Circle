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
        private GUIStyle _buttonStyle;
        
        private int _addMoney;
        private int _spendMoney;
        private int _addHealth;
        private int _spendHealth;

        private readonly Color _orangeColor = new(0.9f, 0.5f, 0.2f);
        
        private bool _gameStarted => EditorApplication.isPlaying;

        [MenuItem("Tools/Cheat-Menu")]
        public static void ShowWindow()
        {
            GetWindow<CheatMenuNewWindow>("Cheat Menu");
        }

        private void OnEnable()
        {
            CreateStyles();
        }

        private void OnGUI()
        {
            EditorGUI.DrawRect(new Rect(0,0, position.width, position.height), Color.black);

            if (!_gameStarted)
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label("Start the game for the menu to appear", _initialStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                ResettingFieldValueAndFindCheatManager();
                return;
            }
            
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                Color originalColor = GUI.color;
                GUI.color = Color.red;
                GUILayout.Button(EditorGUIUtility.IconContent("d_PauseButton").image, GUILayout.Width(35),
                    GUILayout.Height(25));
                GUI.color = originalColor;
                GUI.color = Color.green; 
                GUILayout.Button(EditorGUIUtility.IconContent("d_PlayButton").image, GUILayout.Width(35),
                    GUILayout.Height(25));
                GUI.color = originalColor;
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            Rect horizontalGroup = EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                EditorGUI.DrawRect(horizontalGroup, _orangeColor);

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
                                if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Plus").image,
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
                                        EditorGUIUtility.IconContent("d_Toolbar Minus").image,
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
                                        EditorGUIUtility.IconContent("d_Toolbar Plus").image,
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
                                        EditorGUIUtility.IconContent("d_Toolbar Minus").image,
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
        }

        private void CreateStyles()
        {
            _initialStyle = new GUIStyle()
            {
                normal = { textColor = _orangeColor },
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
            };

            _foldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                normal = { textColor = _orangeColor },
                fontSize = 14,
                alignment = TextAnchor.MiddleLeft,
            };

            _headerStyle = new GUIStyle(EditorStyles.helpBox)
            {
                normal =
                {
                    textColor = _orangeColor
                },
                padding = new RectOffset(10, 10, 5, 5),
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove
            };
        }

        private void ResettingFieldValueAndFindCheatManager()
        {
            Repaint();

            _showEnemySection = false;
            _showMoneySection = false;
            _showHealthSection = false;
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