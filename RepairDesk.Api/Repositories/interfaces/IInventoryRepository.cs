using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Inventory;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface IInventoryRepository
    {
        Task<bool> InventoryExistsAsync(int inventoryId);
        Task<InventoryDetailsModel> GetInventoryAsync(int inventoryId);
        Task<PagedResult<InventoryListModel>> GetInventoriesAsync(int pageNr);
        Task<InventoryDetailsModel> AddInventoryAsync(InventoryAddModel model);
        Task<InventoryDetailsModel> EditInventoryAsync(int inventoryId, InventoryEditModel model);
        Task<bool> DeleteInventoryAsync(int inventoryId);
    }

}
