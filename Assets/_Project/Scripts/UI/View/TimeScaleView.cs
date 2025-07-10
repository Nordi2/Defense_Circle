using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Component
{
    public class TimeScaleView : MonoBehaviour
    {
        [field: SerializeField] public Button PauseButton { get; private set; }
        [field: SerializeField] public Button OneTimeScaleButton { get; private set; }
        [field: SerializeField] public Button TwoTimeScaleButton { get; private set; }
        [field: SerializeField] public Button ThreeTimeScaleButton { get; private set; }
    }
}