using _Project.Scripts.Gameplay.Money;
using _Project.Scripts.Gameplay.Stats.EnemyStats;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Gameplay.EnemyLogic.Callbacks
{
    [UsedImplicitly]
    public class EnemyCallbacks
    {
        private readonly AnimationEnemy _animation;
        private readonly EnemyView _view;
        private readonly Wallet _wallet;
        private readonly MoneyStat _moneyStat;

        public EnemyCallbacks(
            AnimationEnemy animation,
            EnemyView view,
            Wallet wallet,
            MoneyStat moneyStat)
        {
            _animation = animation;
            _view = view;
            _wallet = wallet;
            _moneyStat = moneyStat;
        }

        public void GiveDamageCallback()
        {
            Object.Instantiate(_view.DieEffect, _view.transform.position, Quaternion.identity);
            _wallet.SpendMoney(_moneyStat.SpendMoney);
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
            _wallet.AddMoney(_moneyStat.RewardMoney);
        }
    }
}