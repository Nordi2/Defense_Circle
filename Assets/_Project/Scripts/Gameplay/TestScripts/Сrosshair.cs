using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.TestScripts
{
    public class Сrosshair : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _baseScale;
        [SerializeField] private Vector2 _minScale;

        [SerializeField] private float _maxScaleMultiplier;
        [SerializeField] private float _reductionSpeed;

        private float _sensivity = 0.5f;
        private Vector3 _lastMousePosition;
        private float _currentMultiplay = 1;

        private void Start()
        {
            transform.localScale = _baseScale;
            _lastMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        private void Update()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            transform.position = mousePos;

            float mouseDistance = Vector3.Distance(mousePos, _lastMousePosition);

            if (mouseDistance > 0)
            {
                _currentMultiplay += mouseDistance * _sensivity;
                _currentMultiplay = Math.Min(_currentMultiplay, _maxScaleMultiplier);
            }

            if (_currentMultiplay > _minScale.x * 0.5f)
            {
                _currentMultiplay = Mathf.MoveTowards(
                    _currentMultiplay,
                    _minScale.x * 0.5f,
                    _reductionSpeed * Time.deltaTime);
            }

            _currentMultiplay = Mathf.Max(_currentMultiplay, _minScale.x * 0.5f);
            
            transform.localScale = _baseScale * _currentMultiplay;

            _lastMousePosition = mousePos;
        }
    }
}