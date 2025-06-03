using R3;

namespace _Project.Scripts.Infrastructure.Services.Input
{
    public interface IInputService
    {
        Subject<Unit> OnClickSpaceButton { get; }
    }
}