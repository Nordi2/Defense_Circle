using R3;

namespace _Project.Cor.Spawner
{
    public interface IOpenEventShop
    {
        public Subject<Unit> OnOpenShop { get; }
    }
}