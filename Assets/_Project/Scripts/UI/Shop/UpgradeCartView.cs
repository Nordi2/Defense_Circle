using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UpgradeCartView : MonoBehaviour
    {
        [field: SerializeField] public Button UpgradeButton { get; private set; }
        
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _currentMaxLevelText;
        [SerializeField] private TextMeshProUGUI _nameStatsText;
        
        [SerializeField] private Image _icon;
        
        public void UpdatePrice(string newText) =>
            _priceText.text = newText;

        public void UpdateCurrentLevel(string newText) => 
            _currentMaxLevelText.text = newText;

        public void UpdateNameStats(string newText) =>
            _nameStatsText.text = newText;
        
        public void UpdateIcon(Sprite newIcon) =>
            _icon.sprite = newIcon;
    }
}