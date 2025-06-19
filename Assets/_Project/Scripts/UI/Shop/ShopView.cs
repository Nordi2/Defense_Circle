using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.UI.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private UpgradeCartView _cartViewPrefab;
        [SerializeField] private Transform _container;

        public void OpenShop() =>
            gameObject.SetActive(true);

        public void CloseShop() =>
            gameObject.SetActive(false);

        public UpgradeCartView SpawnCart() =>
            Instantiate(_cartViewPrefab, _container);

        public void UpdateAmountMoney(string value) =>
            _moneyText.text = value;
    }
}