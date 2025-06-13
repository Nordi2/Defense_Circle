using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UpgradeCartView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _icon;

        public void UpdateMoneyText(string newText) => 
            _moneyText.text = newText;

        public void UpdateLevelText(string newText) => 
            _levelText.text = newText;

        public void UpdateNameText(string newText) => 
            _nameText.text = newText;

        public void UpdateIcon(Sprite newIcon) => 
            _icon.sprite = newIcon;
    }
}