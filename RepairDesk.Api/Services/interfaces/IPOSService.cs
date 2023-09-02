using RepairDesk.Api.Models;
using RepairDesk.Api.Models.POS;

namespace RepairDesk.Api.Services.interfaces
{
    public interface IPOSService
    {
        Task<PagedResult<POSListModel>> GetPOSesAsync(int pageNr);
        Task<POSDetailsModel> GetPOSAsync(int posId);
        Task<POSDetailsModel> AddPOSAsync(POSAddModel model);
        Task<POSDetailsModel> EditPOSAsync(int posId, POSEditModel model);
        Task<bool> DeletePOSAsync(int posId);
    }
}
