using UnityEngine;

namespace _Project.Scripts
{
    public class TestMousePosition : MonoBehaviour
    {
        [Header("Настройки масштабирования")]
        [SerializeField] private float sensitivity = 0.001f; // Чувствительность к движению мыши
        [SerializeField] private float reductionSpeed = 2f;   // Скорость уменьшения масштаба
        [SerializeField] private float maxScaleMultiplier = 3f; // Максимальное увеличение

        private Vector3 baseScale;        // Базовый масштаб объекта
        private float currentMultiplier = 1f; // Текущий множитель масштаба
        private Vector3 lastMousePosition; // Позиция мыши в предыдущем кадре

        void Start()
        {
            baseScale = transform.localScale;
            lastMousePosition = Input.mousePosition;
        }

        void Update()
        {
            // Получаем текущую позицию мыши
            Vector3 currentMousePosition = Input.mousePosition;
        
            // Вычисляем расстояние, пройденное мышью с последнего кадра
            float mouseDistance = Vector3.Distance(currentMousePosition, lastMousePosition);
        
            // Увеличиваем множитель при движении мыши
            if (mouseDistance > 0)
            {
                currentMultiplier += mouseDistance * sensitivity;
                currentMultiplier = Mathf.Min(currentMultiplier, maxScaleMultiplier);
            }
        
            // Плавное уменьшение к базовому масштабу
            if (currentMultiplier > 1f)
            {
                currentMultiplier = Mathf.Lerp(
                    currentMultiplier, 
                    1f, 
                    reductionSpeed * Time.deltaTime
                );
            }
        
            // Фиксируем минимальное значение
            currentMultiplier = Mathf.Max(currentMultiplier, 1f);
        
            // Применяем новый масштаб
            transform.localScale = baseScale * currentMultiplier;
        
            // Сохраняем позицию мыши для следующего кадра
            lastMousePosition = currentMousePosition;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        // [SerializeField] private Transform _croshair;
        // [SerializeField] private Camera _camera;
        //
        // private Vector3 _maxСoncentration;
        // private Vector3 _minConcentration = new Vector3(0.5f, 0.5f, 0f);
        //
        // private void Start()
        // {
        //     _maxСoncentration = _croshair.localScale;
        // }

        /*private void Update()
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 vector = _camera.ScreenToWorldPoint(mousePosition);

            _croshair.position = vector;
            
            ScaleCroshar();
        }

        private void UnScaleCroshar()
        {
            
        }

        private void ScaleCroshar()
        {
            _croshair.localScale = Vector3.MoveTowards(_croshair.localScale, _minConcentration, Time.deltaTime);
        }*/
    }
}