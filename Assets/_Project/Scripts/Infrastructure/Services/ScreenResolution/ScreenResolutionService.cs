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
        private readonly Camera _camera;

        public Vector2 MinMaxAxisX { get; private set; }
        public Vector2 MinMaxAxisY { get; private set; }
        
        public ScreenResolutionService(Camera camera)
        {
            _camera = camera;
        }

        void IInitializable.Initialize()
        {
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
        }
    }
}