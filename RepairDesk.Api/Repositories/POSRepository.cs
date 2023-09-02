using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Models.POS;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    public class POSRepository : IPOSRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public POSRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> POSExistsAsync(int posId)
        {
            return await _context.POSes.AnyAsync(p => p.Id == posId);
        }

        public async Task<PagedResult<POSListModel>> GetPOSesAsync(int pageNr = 1)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await _context.POSes.CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var posEntities = await _context.POSes
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(pos => new POSListModel
                {
                    Id = pos.Id,
                    Location = pos.Location,
                    Name = pos.Name
                })
                .ToListAsync();

            return new PagedResult<POSListModel>
            {
                Items = posEntities,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


        public async Task<POSDetailsModel> GetPOSAsync(int posId)
        {
            var pos = await _context.POSes.FirstOrDefaultAsync(p => p.Id == posId);
            if (pos == null)
            {
                throw new ArgumentException($"POS with id {posId} not found.");
            }

            return new POSDetailsModel
            {
                Id = pos.Id,
                Location = pos.Location,
                Name = pos.Name,
            };
        }

        public async Task<POSDetailsModel> AddPOSAsync(POSAddModel model)
        {
            var entity = new POS
            {
                Location = model.Location,
                Name = model.Name
            };

            await _context.POSes.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await GetPOSAsync(entity.Id);
        }

        public async Task<POSDetailsModel> EditPOSAsync(int posId, POSEditModel model)
        {
            var pos = await _context.POSes.FindAsync(posId);
            if (pos == null)
            {
                throw new ArgumentException($"POS with id {posId} not found.");
            }
            if (posId != model.Id)
            {
                throw new ArgumentException($"Id missmatch.");
            }

            // update data
            pos.Location = model.Location;
            pos.Name = model.Name;

            _context.POSes.Update(pos);
            await _context.SaveChangesAsync();

            return await GetPOSAsync(posId);
        }

        public async Task<bool> DeletePOSAsync(int posId)
        {
            var pos = await _context.POSes.FindAsync(posId);
            if (pos == null)
            {
                throw new ArgumentException($"POS with id {posId} not found.");
            }

            _context.POSes.Remove(pos);

            return await _context.SaveChangesAsync() > 0;
        }
    }

}
