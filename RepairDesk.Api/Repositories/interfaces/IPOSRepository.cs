using RepairDesk.Api.Models;
using RepairDesk.Api.Models.POS;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface IPOSRepository
    {
        Task<bool> POSExistsAsync(int posId);
        Task<PagedResult<POSListModel>> GetPOSesAsync(int pageNr);
        Task<POSDetailsModel> GetPOSAsync(int posId);
        Task<POSDetailsModel> AddPOSAsync(POSAddModel model);
        Task<POSDetailsModel> EditPOSAsync(int posId, POSEditModel model);
        Task<bool> DeletePOSAsync(int posId);
    }
}
