using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.EnemyLogic;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class CheatMenuNewWindow : EditorWindow
    {
        private CheatManager _cheatManager;
        private int _addMoney;
        private int _spendMoney;
        private int _addHealth;
        private int _spendHealth;
        
        [MenuItem("Tools/Cheat-Menu")]
        public static void ShowWindow()
        {
            GetWindow<CheatMenuNewWindow>("Cheat Menu");
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayMode;

            if (_cheatManager != null)
            {
                Repaint();
                return;
            }

            ResettingFieldValueAndFindCheatManager();
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayMode;
        }

        private void OnGUI()
        {
            GUILayout.Label("Menu Cheats:", EditorStyles.boldLabel);

            if (_cheatManager != null)
            {
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
                
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.HelpBox("Start the game for the menu to appear", MessageType.Warning);
            }
        }

        private void OnPlayMode(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                ResettingFieldValueAndFindCheatManager();
        }

        private void ResettingFieldValueAndFindCheatManager()
        {
            _cheatManager = FindObjectOfType<CheatManager>();
            Repaint();

            _addMoney = 0;
            _spendMoney = 0;
            _addHealth = 0;
            _spendHealth = 0;
        }

        private void KillAllEnemy() => 
            _cheatManager.KillAllSpawnedEnemies();

        private void KillRandomEnemy() => 
            _cheatManager.KillRandomSpawnedEnemies();

        private void CreateEnemy(EnemyType type) =>
            _cheatManager.SpawnEnemy(type);

        private void LowerHealth() =>
            _cheatManager.TakeDamage(_spendHealth);

        private void SpendMoney()
        {
        }

        private void AddHealthTower()
        {
        }

        private void AddMoney()
        {
        }
    }
}