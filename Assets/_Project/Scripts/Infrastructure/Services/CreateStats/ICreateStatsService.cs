using System.Collections.Generic;
using Meta.Stats;

namespace Infrastructure.Services
{
    public interface ICreateStatsService
    {
        List<Stats> CreateStats();
    }
}