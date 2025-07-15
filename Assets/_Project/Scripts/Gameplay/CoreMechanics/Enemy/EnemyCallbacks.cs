using _Project.Cor.Enemy.Mono;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic.NoneUpgrade;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Enemy
{
    [UsedImplicitly]
    public class EnemyCallbacks
    {
        private readonly AnimationEnemy _animation;
        private readonly EnemyView _view;
        private readonly Wallet _wallet;
        private readonly RewardSpendStat _rewardSpendStat;

        public EnemyCallbacks(
            AnimationEnemy animation,
            EnemyView view,
            Wallet wallet,
            RewardSpendStat rewardSpendStat)
        {
            _animation = animation;
            _view = view;
            _wallet = wallet;
            _rewardSpendStat = rewardSpendStat;
        }

        public void GiveDamageCallback()
        {
            Object.Instantiate(_view.DieEffect, _view.transform.position, Quaternion.identity);
            _wallet.SpendMoney(_rewardSpendStat.SpendMoney);
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
            _wallet.AddMoney(_rewardSpendStat.RewardMoney);
        }
    }
}