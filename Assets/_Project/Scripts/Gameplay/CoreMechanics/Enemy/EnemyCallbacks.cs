using Cor.Enemy.Mono;
using JetBrains.Annotations;
using Meta.Money;
using Meta.Stats.NoneUpgrade;
using UnityEngine;

namespace Cor.Enemy
{
    [UsedImplicitly]
    public class EnemyCallbacks
    {
        private readonly AnimationEnemy _animation;
        private readonly EnemyView _view;
        private readonly Wallet _wallet;
        private readonly RewardStats _rewardStats;

        public EnemyCallbacks(
            AnimationEnemy animation,
            EnemyView view,
            Wallet wallet,
            RewardStats rewardStats)
        {
            _animation = animation;
            _view = view;
            _wallet = wallet;
            _rewardStats = rewardStats;
        }

        public void GiveDamageCallback()
        {
            Object.Instantiate(_view.DieEffect, _view.transform.position, Quaternion.identity);
            _wallet.SpendMoney(_rewardStats.SpendMoney);
        }

        public void TakeDamageCallback(int damageValue)
        {
            _animation.PlayTakeDamageAnimation();

            DamageText damageText = Object.Instantiate(_view.DamageTextPrefab, _view.transform.position,
                _view.transform.rotation);

            damageText.StartAnimation(damageValue);
        }

        public void DieCallback()
        {
            Object.Instantiate(_view.DieEffect, _view.transform.position, Quaternion.identity);
            _wallet.AddMoney(_rewardStats.RewardMoney);
        }
    }
}