using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.TestScripts
{
    public class ShootMine : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabMine;
        [SerializeField] private float _duration;
        [SerializeField] private Vector2 _endPosition;
        [SerializeField] private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject prefabMine = Instantiate(_prefabMine, transform.position, Quaternion.identity);
                Vector3 endPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                endPosition.z = 0;
                prefabMine.transform.DOJump(endPosition, 0.01f, 1, _duration);
            }
        }
    }
}