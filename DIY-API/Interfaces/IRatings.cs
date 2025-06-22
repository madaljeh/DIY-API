using DIY_API.DTOs.Rating;
using DIY_API.Helper;

namespace DIY_API.Interfaces
{
    public interface IRatings
    {
        Task<string> CreateRate(CreateRateDTO input);
        Task<List<GetRateDTO>> GetRateByCallaengeID(int id);
        Task<List<GetRateDTO>> GetRateByUserID(int id);
        Task<List<GetRateDTO>> GetAllRate(PaginationParameters pagination);
        Task<bool> DeleteRate(int Id);
        Task<bool> RateActivationStatus(int RateId, RateActivationDTO input);
    }
}
