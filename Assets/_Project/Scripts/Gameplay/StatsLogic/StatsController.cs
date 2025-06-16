using _Project.Scripts.Gameplay.Stats;
using Zenject;

namespace _Project.Scripts.Gameplay.StatsLogic
{
    public class StatsController :
        IInitializable
    {
        private readonly ICreateStatsService _createStatsService;
        private readonly StatsStorage _statsStorage;

        public StatsController(StatsStorage statsStorage, ICreateStatsService createStatsService)
        {
            _statsStorage = statsStorage;
            _createStatsService = createStatsService;
        }

        void IInitializable.Initialize() => 
            _statsStorage.AddStatsInStorage(_createStatsService.CreateStats());
    }
}