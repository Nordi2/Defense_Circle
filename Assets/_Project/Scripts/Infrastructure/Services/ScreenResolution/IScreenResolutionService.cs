using UnityEngine;

namespace Infrastructure.Services.Services.ScreenResolution
{
    public interface IScreenResolutionService
    {
        Vector2 MinMaxAxisX { get; }
        Vector2 MinMaxAxisY { get; }
    }
}