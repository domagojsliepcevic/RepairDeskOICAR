using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Repair;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;

namespace RepairDesk.Api.Services
{
    public class RepairService : IRepairService
    {
        private readonly IRepairRepository _repairRepository;
        private readonly IUserRepository _userRepository;

        public RepairService(IRepairRepository repairRepository, IUserRepository userRepository)
        {
            _repairRepository = repairRepository;
            _userRepository = userRepository;
        }

        public async Task<PagedResult<RepairListModel>> GetRepairsByUserAsync(int userId, int pageNr = 1)
        {
            return await _repairRepository.GetRepairsAsync(pageNr, userId);
        }

        public async Task<RepairDetailsModel> GetRepairByUserAsync(int userId, int repairId)
        {
            return await _repairRepository.GetRepairAsync(repairId, userId);
        }

		public async Task<PagedResult<RepairListModel>> GetRepairsAsync(int pageNr = 1)
		{
			return await _repairRepository.GetRepairsAsync(pageNr: pageNr);
		}

		public async Task<RepairDetailsModel> GetRepairAsync(int repairId)
		{
			return await _repairRepository.GetRepairAsync(repairId: repairId);
		}

		public async Task<RepairDetailsModel> AddRepairAsync(RepairAddModel model)
        {
            return await _repairRepository.AddRepairAsync(model);
        }

        //public async Task<RepairDetailsModel> EditRepairAsync(int userId, int reapirId, RepairEditModel model)
        //{
        //    if (await _userRepository.UserExistsAsync(model.UserId))
        //    {
        //        throw new ArgumentException($"User with id {model.UserId} not found");
        //    }
        //    // todo check if status exists

        //    return await _repairRepository.EditRepairAsync(userId, reapirId, model);
        //}

        public async Task<bool> DeleteRepairAsync(int userId, int repairId)
        {
            return await _repairRepository.DeleteRepairAsync(userId, repairId);
        }

        public async Task<PagedResult<RepairListModel>> GetRepairsByPOSAsync(int posId, int pageNr = 1)
        {
            return await _repairRepository.GetRepairsByPOSAsync(posId, pageNr);
        }

        public async Task<RepairDetailsModel> GetRepairByPOSAsync(int posId, int repairId)
        {
            return await _repairRepository.GetRepairByPOSAsync(posId, repairId);
        }
    }

}
