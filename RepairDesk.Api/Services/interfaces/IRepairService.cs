using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Repair;

namespace RepairDesk.Api.Services.interfaces
{
    public interface IRepairService
    {
        Task<PagedResult<RepairListModel>> GetRepairsByUserAsync(int userId, int pageNr);
        Task<RepairDetailsModel> GetRepairByUserAsync(int userId, int repairId);
		Task<PagedResult<RepairListModel>> GetRepairsAsync(int pageNr);
		Task<RepairDetailsModel> GetRepairAsync(int repairId);
		Task<RepairDetailsModel> AddRepairAsync(RepairAddModel model);
        //Task<RepairDetailsModel> EditRepairAsync(int id, RepairEditModel model);
        Task<bool> DeleteRepairAsync(int userId, int repairId);
        Task<PagedResult<RepairListModel>> GetRepairsByPOSAsync(int posId, int orderId);
        Task<RepairDetailsModel> GetRepairByPOSAsync(int posId, int repairId);

    }
}
