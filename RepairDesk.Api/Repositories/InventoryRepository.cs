using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Inventory;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    //public class InventoryRepository : IInventoryRepository
    //{
    //    private readonly AppDbContext _context;
    //    private readonly IConfiguration _configuration;

    //    public InventoryRepository(AppDbContext context, IConfiguration configuration)
    //    {
    //        _context = context;
    //        _configuration = configuration;
    //    }

    //    public async Task<bool> InventoryExistsAsync(int inventoryId)
    //    {
    //        return await _context.Inventories.AnyAsync(i => i.Id == inventoryId);
    //    }

    //    public async Task<InventoryDetailsModel> GetInventoryAsync(int inventoryId)
    //    {
    //        var entity = await _context.Inventories.Where(i => i.Id == inventoryId).FirstOrDefaultAsync();
    //        if (entity == null)
    //        {
    //            throw new ArgumentException($"Inventory with id {inventoryId} not found.");
    //        }

    //        return new InventoryDetailsModel
    //        {
    //            Id = entity.Id,
    //            Quantity = entity.Quantity,
    //            ProductId = entity.ProductId,
    //            ProductName = entity.Product.Name,
    //            POSId = entity.POSId,
    //            POSName = entity.POS.Name
    //        };
    //    }

    //    public async Task<PagedResult<InventoryListModel>> GetInventoriesAsync(int pageNr = 1)
    //    {
    //        int pageSize = int.Parse(_configuration["Pages:Size"]);
    //        int totalItems = await _context.Inventories.CountAsync();
    //        int totalPages = (totalItems + pageSize - 1) / pageSize;

    //        if (pageNr < 1) pageNr = 1;
    //        if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

    //        var inventories = await _context.Inventories
    //            .Include(i => i.Product)
    //            .Include(i => i.POS)
    //            .Skip((pageNr - 1) * pageSize)
    //            .Take(pageSize)
    //            .Select(i => new InventoryListModel
    //            {
    //                Id = i.Id,
    //                Quantity = i.Quantity,
    //                ProductId = i.ProductId,
    //                ProductName = i.Product.Name,
    //                POSId = i.POSId,
    //                POSName = i.POS.Name
    //            })
    //            .ToListAsync();

    //        return new PagedResult<InventoryListModel>
    //        {
    //            Items = inventories,
    //            CurrentPage = pageNr,
    //            TotalPages = totalPages
    //        };
    //    }


    //    public async Task<InventoryDetailsModel> AddInventoryAsync(InventoryAddModel model)
    //    {
    //        var entity = new Inventory
    //        {
    //            Quantity = model.Quantity,
    //            ProductId = model.ProductId,
    //            POSId = model.POSId
    //        };

    //        _context.Inventories.Add(entity);
    //        await _context.SaveChangesAsync();

    //        return await GetInventoryAsync(entity.Id);
    //    }

    //    public async Task<InventoryDetailsModel> EditInventoryAsync(int inventoryId, InventoryEditModel model)
    //    {
    //        var inventory = await _context.Inventories.FindAsync(model.Id);
    //        if (inventory == null)
    //        {
    //            throw new ArgumentException($"Inventory with id {inventoryId} not found.");
    //        }
    //        if (inventoryId != model.Id)
    //        {
    //            throw new ArgumentException($"Id missmatch.");
    //        }

    //        // update data
    //        inventory.Quantity = model.Quantity;
    //        inventory.ProductId = model.ProductId;
    //        inventory.POSId = model.POSId;

    //        _context.Entry(inventory).State = EntityState.Modified;
    //        await _context.SaveChangesAsync();

    //        return await GetInventoryAsync(inventory.Id);
    //    }

    //    public async Task<bool> DeleteInventoryAsync(int inventoryId)
    //    {
    //        var inventory = await _context.Inventories.FindAsync(inventoryId);
    //        if (inventory == null)
    //        {
    //            throw new ArgumentException($"Inventory with id {inventoryId} not found.");
    //        }

    //        _context.Inventories.Remove(inventory);

    //        return await _context.SaveChangesAsync() > 0;
    //    }
    //}
}
