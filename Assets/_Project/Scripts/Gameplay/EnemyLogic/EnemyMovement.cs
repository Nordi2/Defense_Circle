using _Project.Scripts.Gameplay.TowerLogic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    [UsedImplicitly]
    public class EnemyMovement :
        ITickable
    {
        private readonly Vector2 _targetPosition;
        private readonly Transform _objectTransform;
        private readonly float _moveSpeed;

        public EnemyMovement(
            IGetTargetPosition getTarget,
            Transform objectTransform,
            EnemyStats stats)
        {
            _targetPosition = getTarget.GetPosition();
            _objectTransform = objectTransform;
            _moveSpeed = stats.MovementSpeed;
        }

        void ITickable.Tick()
        {
            _objectTransform.position = Vector3.MoveTowards(
                current: _objectTransform.position,
                target: _targetPosition,
                maxDistanceDelta: _moveSpeed * Time.deltaTime);
        }
    }
}