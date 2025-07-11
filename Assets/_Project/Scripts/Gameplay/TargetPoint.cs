using _Project.Cor.Interfaces;
using UnityEngine;
using Zenject;

public class TargetPoint : MonoBehaviour,
    IGetTargetPosition
{
    private Vector2 _spawnPoint;

    [Inject]
    private void Construct(Vector2 spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    public Vector2 GetPosition() =>
        _spawnPoint;
}