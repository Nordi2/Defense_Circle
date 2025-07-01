using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Services.ScreenResolution
{
    [UsedImplicitly]
    public class ScreenResolutionService :
        IInitializable,
        IScreenResolutionService
    {
        private Camera _camera;

        public Vector2 MinMaxAxisX { get; private set; }
        public Vector2 MinMaxAxisY { get; private set; }
        
        public float ScreenHeight { get; private set; }
        public float ScreenWidth { get; private set; }

        void IInitializable.Initialize()
        {
            _camera = Camera.main;

            float screenAspect = (float)Screen.width / Screen.height;
            float cameraHeight = _camera.orthographicSize;

            float cameraWidth = cameraHeight * screenAspect;
            Vector2 cameraCenter = _camera.transform.position;

            float minX = cameraCenter.x - cameraWidth;
            float maxX = cameraCenter.x + cameraWidth;
            float minY = cameraCenter.y - cameraHeight;
            float maxY = cameraCenter.y + cameraHeight;

            MinMaxAxisX = new Vector2(minX, maxX);
            MinMaxAxisY = new Vector2(minY, maxY);

            ScreenHeight = _camera.orthographicSize * 2f;
            ScreenWidth = ScreenHeight * _camera.aspect;
        }
    }
}