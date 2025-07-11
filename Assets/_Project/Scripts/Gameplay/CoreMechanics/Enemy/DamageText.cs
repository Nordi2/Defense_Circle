using TMPro;
using DG.Tweening;
using UnityEngine;

namespace _Project.Cor.Enemy
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textDamage;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _offsetYPosition;
        [SerializeField] private Ease _ease;
        
        private Sequence _sequence;

        public void StartAnimation(int damage)
        {
            _textDamage.text = damage.ToString();
            
            _sequence = DOTween.Sequence();

            _sequence
                .Append(_textDamage.transform.DOMoveY(_textDamage.transform.position.y + _offsetYPosition, _animationDuration)
                    .SetEase(_ease))
                .Join(_textDamage.DOFade(0f, _animationDuration))
                .OnComplete(() => gameObject.SetActive(false))
                .Play();
        }
    }
}