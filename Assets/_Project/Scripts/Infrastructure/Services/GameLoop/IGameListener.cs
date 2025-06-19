namespace Infrastructure.Services
{
    public interface IGameListener
    { }

    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnGamePause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnGameResume();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnGameFinish();
    }
}