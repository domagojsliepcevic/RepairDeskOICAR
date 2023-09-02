using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.POS;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;

namespace RepairDesk.Api.Services
{
    public class POSService : IPOSService
    {
        private readonly IPOSRepository _posRepository;

        public POSService(IPOSRepository posRepository)
        {
            _posRepository = posRepository;
        }

        public async Task<PagedResult<POSListModel>> GetPOSesAsync(int pageNr = 1)
        {
            return await _posRepository.GetPOSesAsync(pageNr);
        }

        public async Task<POSDetailsModel> GetPOSAsync(int id)
        {
            return await _posRepository.GetPOSAsync(id);
        }

        public async Task<POSDetailsModel> AddPOSAsync(POSAddModel model)
        {
            return await _posRepository.AddPOSAsync(model);
        }

        public async Task<POSDetailsModel> EditPOSAsync(int id, POSEditModel model)
        {
            return await _posRepository.EditPOSAsync(id, model);
        }

        public async Task<bool> DeletePOSAsync(int id)
        {
            return await _posRepository.DeletePOSAsync(id);
        }
    }
}
