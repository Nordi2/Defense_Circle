using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Extensions
{
    public static class ButtonExtension
    {
        public static void AddListener(this Button button, UnityAction unityAction) =>
            button.onClick.AddListener(unityAction);

        public static void RemoveListener(this Button button, UnityAction unityAction) =>
            button.onClick.RemoveListener(unityAction);

        public static void RemoveAllListeners(this Button button) =>
            button.onClick.RemoveAllListeners();
    }
}