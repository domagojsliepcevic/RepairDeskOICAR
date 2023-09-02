using RepairDesk.Api.Models.Stats;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface IStatsRepository
    {
        Task<StatsModel> GetStatsAsync();
    }
}
