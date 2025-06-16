using Cor.Interfaces;
using JetBrains.Annotations;
using Meta.Stats.NoneUpgrade;
using UnityEngine;
using Zenject;

namespace Cor.Enemy
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
            MoveSpeedStats moveSpeedStats)
        {
            _targetPosition = getTarget.GetPosition();
            _objectTransform = objectTransform;
            _moveSpeed = moveSpeedStats.Speed;
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