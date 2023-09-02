using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models.Stats;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly AppDbContext _context;

        public StatsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StatsModel> GetStatsAsync()
        {
            var nrOfUsers = await _context.Users.CountAsync();
            var nrOfProducts = await _context.Products.CountAsync();
            var nrOfOrders = await _context.Orders.CountAsync();
            var nrOfRepairs = await _context.Repairs.CountAsync();
            var nrOffCategories = await _context.ProductCategories.CountAsync();

            StatsModel model = new StatsModel
            {
                NrOfCategories = nrOffCategories,
                NrOfUsers = nrOfUsers,
                NrOfProducts = nrOfProducts,
                NrOfOrders = nrOfOrders,
                NrOfRepairs = nrOfRepairs
            };

            return model;
        }
    }
}
