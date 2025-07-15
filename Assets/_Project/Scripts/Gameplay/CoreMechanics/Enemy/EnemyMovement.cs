using _Project.Cor.Interfaces;
using _Project.Meta.StatsLogic.NoneUpgrade;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Cor.Enemy
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
            MoveSpeedStat moveSpeedStat)
        {
            _targetPosition = getTarget.GetPosition();
            _objectTransform = objectTransform;
            _moveSpeed = moveSpeedStat.Speed;
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