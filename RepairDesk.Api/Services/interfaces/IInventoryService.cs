using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Inventory;

namespace RepairDesk.Api.Services.interfaces
{
    public interface IInventoryService
    {
        Task<InventoryDetailsModel> GetInventoryAsync(int inventoryId);

        Task<PagedResult<InventoryListModel>> GetInventoriesAsync(int pageNr);

        Task<InventoryDetailsModel> AddInventoryAsync(InventoryAddModel model);

        Task<InventoryDetailsModel> EditInventoryAsync(int inventoryId, InventoryEditModel editModel);

        Task<bool> DeleteInventoryAsync(int inventoryId);
    }
}
