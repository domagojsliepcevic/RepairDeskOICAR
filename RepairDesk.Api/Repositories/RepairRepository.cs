using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Repair;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    public class RepairRepository : IRepairRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public RepairRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> RepairExistsAsync(int repairId)
        {
            return await _context.Repairs.AnyAsync(r => r.Id == repairId);
        }

        public async Task<PagedResult<RepairListModel>> GetRepairsAsync(int pageNr = 1, int? userId = null)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);

            var query = _context.Repairs
				.Include(r => r.User)
				.Include(r => r.Status)
				.AsQueryable();

            if (userId != null)
            {
				query = query.Where(r => r.UserId == userId);
			}

            int totalItems = await query.CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var repairs = await query
				.Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(repair => new RepairListModel
                {
                    Id = repair.Id,
                    RepairNumber = repair.RepairNumber,
                    RequestDate = repair.RequestDate,
                    StatusId = repair.StatusId,
                    Status = repair.Status.Name,
                    Description = repair.Description,
                    FinishDate = repair.FinishDate,
                    UserId = repair.UserId,
                    User = $"{repair.User.FirstName} {repair.User.LastName}"
                })
                .ToListAsync();

            return new PagedResult<RepairListModel>
            {
                Items = repairs,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


        public async Task<RepairDetailsModel> GetRepairAsync(int repairId, int? userId = null)
        {
			var query = _context.Repairs
	            .Include(r => r.User)
	            .Include(r => r.Status)
				.Where(r => r.Id == repairId)
				.AsQueryable();

			if (userId != null)
			{
				query.Where(r => r.UserId == userId);
			}

			var repair = await query.FirstOrDefaultAsync();
            if (repair == null)
            {
                throw new ArgumentException($"Repair with id '{repairId}' not found.");
            }

            return new RepairDetailsModel
            {
                Id = repair.Id,
                RepairNumber = repair.RepairNumber,
                RequestDate = repair.RequestDate,
                StatusId = repair.StatusId,
                Status = repair.Status.Name,
                Description = repair.Description,
                FinishDate = repair.FinishDate,
                UserId = repair.UserId,
                User = $"{repair.User.FirstName} {repair.User.LastName}"
            };
        }


		public async Task<RepairDetailsModel> AddRepairAsync(RepairAddModel model)
        {
            var repair = new Repair
            {
                RepairNumber = Guid.NewGuid().ToString(),
                RequestDate = model.RequestDate,
                StatusId = (int)RepairStatuses.REQUEST_RECEIVED,
                Description = model.Description,
                UserId = model.UserId,
                POSId = model.POSId
            };

            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();

            return await GetRepairAsync(repair.Id);
        }

        //public async Task<RepairDetailsModel> EditRepairAsync(int repairId, RepairEditModel model)
        //{
        //    var repair = await _context.Repairs.FindAsync(repairId);
        //    if (repair == null)
        //    {
        //        throw new ArgumentException($"Repair with id {repairId} not found.");
        //    }
        //    if (repairId != model.Id)
        //    {
        //        throw new ArgumentException($"Repair id missmatch.");
        //    }

        //    // update data
        //    repair.RequestDate = model.RequestDate;
        //    repair.StatusId = model.StatusId;
        //    repair.Description = model.Description;
        //    repair.EstimatedRepairFinishDate = model.EstimatedRepairFinishDate;
        //    repair.UserId = model.UserId;

        //    _context.Entry(repair).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return await GetRepairAsync(repair.Id);
        //}

        public async Task<bool> DeleteRepairAsync(int userId, int repairId)
        {
            var repair = await _context.Repairs.FindAsync(repairId);
            if (repair == null)
            {
                throw new ArgumentException($"Repair with id {repairId} not found.");
            }

            _context.Repairs.Remove(repair);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<RepairListModel>> GetRepairsByPOSAsync(int posId, int pageNr = 1)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await _context.Repairs
                .Where(r => r.POSId == posId)
                .CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var repairs = await _context.Repairs
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Where(r => r.POSId == posId)
                .Select(repair => new RepairListModel
                {
                    Id = repair.Id,
                    RepairNumber = repair.RepairNumber,
                    RequestDate = repair.RequestDate,
                    StatusId = repair.StatusId,
                    Status = repair.Status.Name,
                    Description = repair.Description,
                    FinishDate = repair.FinishDate,
                    UserId = repair.UserId,
                    User = $"{repair.User.FirstName} {repair.User.LastName}"
                })
                .ToListAsync();

            return new PagedResult<RepairListModel>
            {
                Items = repairs,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }

        public async Task<RepairDetailsModel> GetRepairByPOSAsync(int posId, int repairId)
        {
            var repair = await _context.Repairs
                .Include(r => r.User)
                .Include(r => r.Status)
                .Where(r => r.POSId == posId && r.Id == repairId)
                .FirstOrDefaultAsync();
            if (repair == null)
            {
                throw new ArgumentException($"Repair with id {repairId} not found.");
            }

            return new RepairDetailsModel
            {
                Id = repair.Id,
                RepairNumber = repair.RepairNumber,
                RequestDate = repair.RequestDate,
                StatusId = repair.StatusId,
                Status = repair.Status.Name,
                Description = repair.Description,
                FinishDate = repair.FinishDate,
                UserId = repair.UserId,
                User = $"{repair.User.FirstName} {repair.User.LastName}"
            };
        }

    }

}
