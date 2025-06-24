using System;
using System.Collections.Generic;
using _Project.Cor.Enemy;
using _Project.Meta.StatsLogic.Upgrade;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class CheatMenuNewWindow : EditorWindow
    {
        private bool _showMoneySection;
        private bool _showHealthSection;
        private bool _showEnemySection;
        private bool _showStatsSection;
        private bool _showUISection;

        private readonly Dictionary<Type, bool> _foldoutStates = new();

        private GUIStyle _initialStyle;
        private GUIStyle _headerGroupStyle;
        private GUIStyle _headerStyle;
        private GUIStyle _foldoutStyle;
        private GUIStyle _buttonStyle;

        private int _addMoney;
        private int _spendMoney;
        private int _addHealth;
        private int _spendHealth;

        private readonly Color _orangeColor = new(0.9f, 0.5f, 0.2f);

        private bool _gameStarted => EditorApplication.isPlaying;

        [MenuItem("Tools/Cheats-Window")]
        public static void ShowWindow()
        {
            GetWindow<CheatMenuNewWindow>("Cheats-Window");
        }

        private void OnEnable()
        {
            CreateStyles();
        }

        private void OnGUI()
        {
            EditorGUI.DrawRect(new Rect(0, 0, position.width, position.height), Color.black);

            if (!_gameStarted)
            {
                InitialPreview();
                ResettingFieldValueAndFindCheatManager();
                return;
            }

            PauseAndExitButton();

            Rect horizontalGroup = EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                EditorGUI.DrawRect(horizontalGroup, _orangeColor);

                RenderingCheatMenu();
                GUILayout.Space(5);

                RenderingGameInfo();

                GUILayout.Space(5);

                RenderingTowerInfo();
            }
            GUILayout.EndHorizontal();
        }

        private void RenderingTowerInfo()
        {
            Rect verticalGroupTowerInfo =
                EditorGUILayout.BeginVertical(GUI.skin.window, GUILayout.Width(400));
            {
                EditorGUI.DrawRect(verticalGroupTowerInfo, Color.black);
                GUILayout.Label("Tower Info", _headerGroupStyle, GUILayout.ExpandWidth(true));

                if (CheatManager.Wallet is not null)
                {
                    GUILayout.Label("Wallet", _headerStyle, GUILayout.ExpandWidth(true));

                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    {
                        GUILayout.Label(
                            $"Current money: {CheatManager.Wallet.CurrentMoney.CurrentValue.ToString()}",
                            _headerStyle, GUILayout.ExpandWidth(true));
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Label("Stats", _headerStyle, GUILayout.ExpandWidth(true));

                    for (int i = 0; i < CheatManager.StatsStorage.StatsList.Count; i++)
                    {
                        Stats stats = CheatManager.StatsStorage.StatsList[i];
                        RenderingStats(stats);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void RenderingStats(Stats stats)
        {
            if (!_foldoutStates.TryGetValue(stats.GetType(), out bool isOpen))
            {
                isOpen = false;
                _foldoutStates[stats.GetType()] = isOpen;
            }

            GUI.color = _orangeColor;
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            {
                isOpen =
                    EditorGUILayout.BeginFoldoutHeaderGroup(isOpen, stats.GetType().Name);
                _foldoutStates[stats.GetType()] = isOpen;
                {
                    if (isOpen)
                    {
                        RenderingValueStats();
                    }
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            GUILayout.EndHorizontal();

            void RenderingValueStats()
            {
                GUILayout.Label($"Current Level: {stats.CurrentLevel}\n" +
                                $"Max Level: {stats.MaxLevel}\n" +
                                $"Value: {stats.CurrentValue}",
                    EditorStyles.boldLabel);
            }
        }

        private void RenderingGameInfo()
        {
            Rect verticalGroupGameInfo =
                EditorGUILayout.BeginVertical(GUI.skin.window, GUILayout.ExpandWidth(true));
            {
                EditorGUI.DrawRect(verticalGroupGameInfo, Color.black);
                GUILayout.Label("Game Info", _headerGroupStyle, GUILayout.ExpandWidth(true));
            }
            EditorGUILayout.EndVertical();
        }

        private void InitialPreview()
        {
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Start the game for the menu to appear", _initialStyle,
                        GUILayout.ExpandWidth(true));
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUI.color = _orangeColor;
                    if (GUILayout.Button(EditorGUIUtility.IconContent("d_PlayButton").image,
                            GUILayout.ExpandWidth(true), GUILayout.Width(45), GUILayout.Height(35)))
                    {
                        EditorApplication.isPlaying = true;
                    }

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }

        private void RenderingCheatMenu()
        {
            Rect verticalGroupCheatMenu = EditorGUILayout.BeginVertical("window", GUILayout.Width(200));
            {
                EditorGUI.DrawRect(verticalGroupCheatMenu, Color.black);
                GUILayout.Label("Cheat-Menu", _headerGroupStyle, GUILayout.ExpandWidth(true));

                GUILayout.Space(10);

                EditorGUI.BeginDisabledGroup(!CheatManager.ActivateCheats);
                {
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
                                    CheatManager.AddMoney(_addMoney);

                                _addMoney = EditorGUILayout.IntField(_addMoney, EditorStyles.toolbarTextField,
                                    GUILayout.Height(30));
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.Space(5);

                            GUILayout.BeginHorizontal(EditorStyles.toolbar);
                            {
                                if (GUILayout.Button(
                                        EditorGUIUtility.IconContent("d_Toolbar Minus").image,
                                        EditorStyles.toolbarButton,
                                        GUILayout.Height(30)))
                                    CheatManager.SpendMoney(_spendMoney);

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
                        EditorGUILayout.BeginFoldoutHeaderGroup(_showHealthSection, "Health Section",
                            _foldoutStyle);
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
                                    CheatManager.HealPlayer(_addHealth);

                                _addHealth = EditorGUILayout.IntField(_addHealth, EditorStyles.toolbarTextField,
                                    GUILayout.Height(30));
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.Space(5);

                            GUILayout.BeginHorizontal(EditorStyles.toolbar);
                            {
                                if (GUILayout.Button(
                                        EditorGUIUtility.IconContent("d_Toolbar Minus").image,
                                        EditorStyles.toolbarButton,
                                        GUILayout.Height(30)))
                                    CheatManager.TakeDamage(_spendHealth);

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
                                CheatManager.SpawnEnemy(EnemyType.Fast);
                            if (GUILayout.Button("Create Default Enemy", EditorStyles.miniButton))
                                CheatManager.SpawnEnemy(EnemyType.Default);
                            if (GUILayout.Button("Create Slow Enemy", EditorStyles.miniButton))
                                CheatManager.SpawnEnemy(EnemyType.Slow);

                            EditorGUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Kill Random Enemy", EditorStyles.miniButton))
                                    CheatManager.KillRandomSpawnedEnemies();

                                if (GUILayout.Button("Kill All Enemy", EditorStyles.miniButton))
                                    CheatManager.KillAllSpawnedEnemies();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();
                    }

                    #endregion

                    #region StatsSection

                    _showStatsSection =
                        EditorGUILayout.BeginFoldoutHeaderGroup(_showStatsSection, "Stats Section", _foldoutStyle);
                    EditorGUILayout.EndFoldoutHeaderGroup();

                    if (_showStatsSection)
                    {
                    
                    }

                    #endregion

                    #region UISection

                    _showUISection =
                        EditorGUILayout.BeginFoldoutHeaderGroup(_showUISection, "UI Section", _foldoutStyle);
                    EditorGUILayout.EndFoldoutHeaderGroup();

                    if (_showUISection)
                    {
                        GUILayout.BeginVertical();
                        {
                            if (GUILayout.Button("OpenShop"))
                                CheatManager.OpenShop();

                            if (GUILayout.Button("CloseShop"))
                                CheatManager.CloseShop();
                        }
                        GUILayout.EndVertical();
                    }

                    #endregion
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndVertical();
        }

        private void PauseAndExitButton()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                Color originalColor = GUI.color;

                GUI.color = Color.magenta;
                if (GUILayout.Button(EditorGUIUtility.IconContent("d_PauseButton").image, GUILayout.Width(35),
                        GUILayout.Height(25)))
                    EditorApplication.isPaused = !EditorApplication.isPaused;

                GUI.color = Color.red;
                if (GUILayout.Button(EditorGUIUtility.IconContent("Cancel").image, GUILayout.Width(35),
                        GUILayout.Height(25)))
                    EditorApplication.isPlaying = false;

                GUI.color = originalColor;
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }

        private void CreateStyles()
        {
            _initialStyle = new GUIStyle()
            {
                normal = { textColor = _orangeColor },
                fontSize = 15,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
            };

            _foldoutStyle = new GUIStyle(EditorStyles.foldoutHeader)
            {
                normal = { textColor = _orangeColor },
                fontSize = 14,
                alignment = TextAnchor.MiddleLeft,
            };

            _headerGroupStyle = new GUIStyle(EditorStyles.selectionRect)
            {
                normal =
                {
                    textColor = _orangeColor
                },
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove
            };

            _headerStyle = new GUIStyle(EditorStyles.whiteLabel)
            {
                normal =
                {
                    textColor = _orangeColor
                },

                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
            };
        }

        private void ResettingFieldValueAndFindCheatManager()
        {
            Repaint();

            CheatManager.StaticZero();

            _showEnemySection = false;
            _showMoneySection = false;
            _showHealthSection = false;

            _addMoney = 0;
            _spendMoney = 0;
            _addHealth = 0;
            _spendHealth = 0;
        }
    }
}