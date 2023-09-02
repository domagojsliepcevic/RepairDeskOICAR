using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Repair;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface IRepairRepository
    {
        Task<bool> RepairExistsAsync(int repairId);
        Task<PagedResult<RepairListModel>> GetRepairsAsync(int pageNr = 1, int? userId=null);
        Task<RepairDetailsModel> GetRepairAsync(int repairId, int? userId=null);
		Task<RepairDetailsModel> AddRepairAsync(RepairAddModel model);
        //Task<RepairDetailsModel> EditRepairAsync(int repairId, RepairEditModel model);
        Task<bool> DeleteRepairAsync(int userId, int repairId);
        Task<PagedResult<RepairListModel>> GetRepairsByPOSAsync(int posId, int orderId);
        Task<RepairDetailsModel> GetRepairByPOSAsync(int posId, int repairId);
    }
}
