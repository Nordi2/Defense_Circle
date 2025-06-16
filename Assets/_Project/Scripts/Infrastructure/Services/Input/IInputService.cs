using R3;

namespace Infrastructure.Services
{
    public interface IInputService
    {
        Subject<Unit> OnClickSpaceButton { get; }
    }
}