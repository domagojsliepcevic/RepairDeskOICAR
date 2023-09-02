using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Stats;
using RepairDesk.Api.Repositories;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services;
using RepairDesk.Api.Services.interfaces;

namespace RepairDesk.Api.Services
{
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statsRepository;

        public StatsService(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task<StatsModel> GetStatsAsync()
        {
            return await _statsRepository.GetStatsAsync();
        }
    }
}

