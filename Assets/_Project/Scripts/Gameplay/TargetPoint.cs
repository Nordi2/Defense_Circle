using _Project.Cor.Interfaces;
using UnityEngine;

public class TargetPoint : MonoBehaviour , 
    IGetTargetPosition
{
    [SerializeField] private Vector2 _spawnPoint;
    
    public Vector2 GetPosition() => 
        _spawnPoint;
}
