using UnityEngine;

namespace _Project.Scripts.UI
{
    public class UIRoot : MonoBehaviour
    {
        public RectTransform Container { get;private set; }
        [SerializeField] private RectTransform _container;

        public void AddToContainer(RectTransform addUIObject) =>
            addUIObject.SetParent(_container, false);

        public void RemoveChildren()
        {
            for (int i = 0; i < _container.childCount; i++)
            {
                Transform child = _container.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}