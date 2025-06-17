using Infrastructure.Services;
using Zenject;

namespace _Project.Meta.Stats
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