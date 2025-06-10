using _Project.Scripts.Gameplay.Tower;
using UnityEngine;

public class TargetPoint : MonoBehaviour , 
    IGetTargetPosition
{
    [SerializeField] private Vector2 _spawnPoint;
    
    public Vector2 GetPosition() => 
        _spawnPoint;
}
