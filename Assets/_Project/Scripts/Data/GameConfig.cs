using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "GameConfig",
        menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public TowerConfig TowerConfig { get; private set; }
        [field: SerializeField] public SpawnerConfig SpawnerConfig { get; private set; }
    }
}