using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Inventory;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;

namespace RepairDesk.Api.Services
{
    //public class InventoryService : IInventoryService
    //{
    //    private readonly IInventoryRepository _inventoryRepository;


    //    public InventoryService(IInventoryRepository inventoryRepository)
    //    {
    //        _inventoryRepository = inventoryRepository;
    //    }


    //    public async Task<InventoryDetailsModel> GetInventoryAsync(int inventoryId)
    //    {
    //        return await _inventoryRepository.GetInventoryAsync(inventoryId);
    //    }

    //    public async Task<PagedResult<InventoryListModel>> GetInventoriesAsync(int pageNr = 1)
    //    {
    //        return await _inventoryRepository.GetInventoriesAsync(pageNr);
    //    }

    //    public async Task<InventoryDetailsModel> AddInventoryAsync(InventoryAddModel model)
    //    {
    //        return await _inventoryRepository.AddInventoryAsync(model);
    //    }

    //    public async Task<InventoryDetailsModel> EditInventoryAsync(int inventoryId, InventoryEditModel model)
    //    {
    //        return await _inventoryRepository.EditInventoryAsync(inventoryId, model);
    //    }

    //    public async Task<bool> DeleteInventoryAsync(int inventoryId)
    //    {
    //        return await _inventoryRepository.DeleteInventoryAsync(inventoryId);
    //    }

    //}
}
