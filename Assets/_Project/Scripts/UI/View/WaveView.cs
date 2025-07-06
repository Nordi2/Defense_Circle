using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Component
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeToNextWave;
        [SerializeField] private TextMeshProUGUI _currentWave;
        [SerializeField] private Image _progressBar;
        
    }
}