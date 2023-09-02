using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Stats;

namespace RepairDesk.Api.Services.interfaces
{
    public interface IStatsService
    {
        Task<StatsModel> GetStatsAsync();

    }
}
