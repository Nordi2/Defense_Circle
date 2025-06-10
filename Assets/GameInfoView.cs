using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.UI.View;
using UnityEngine;

public class GameInfoView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private WalletView _walletView;

    public void StartAnimation()
    {
        _walletView.StartAnimation();
    }
}