using DIY_API.DTOs.Rating;

namespace DIY_API.Interfaces
{
    public interface IRatings
    {
        Task<string> CreateRate(CreateRateDTO input);
        Task<List<GetRateDTO>> GetRateByCallaengeID(int id);
        Task<List<GetRateDTO>> GetRateByUserID(int id);
    }
}
