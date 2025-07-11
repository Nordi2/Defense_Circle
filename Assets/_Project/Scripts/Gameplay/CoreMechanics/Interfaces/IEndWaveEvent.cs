using R3;

namespace _Project.Cor.Spawner
{
    public interface IEndWaveEvent
    {
        Subject<Unit> OnEndWave { get; }
    }
}