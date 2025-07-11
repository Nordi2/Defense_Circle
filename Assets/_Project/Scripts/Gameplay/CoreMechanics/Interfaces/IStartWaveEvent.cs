using R3;

namespace _Project.Cor.Spawner
{
    public interface IStartWaveEvent
    {
        Subject<Unit> OnStartWave { get; }
    }
}