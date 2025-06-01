using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Observers
{
    public class TriggerObserver<T> : MonoBehaviour
    {
        private readonly Subject<T> _triggerEnter = new();

        public Observable<T> TriggerEnter => _triggerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out T component)) 
                _triggerEnter.OnNext(component);
        }
    }
}