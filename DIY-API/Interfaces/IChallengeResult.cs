using DIY_API.DTOs.ChallengeResultDTO;

namespace DIY_API.Interfaces
{
    public interface IChallengeResult
    {
        Task<List<GetChallengeResultDTO>> GetAllChallengeResult();
        Task<List<GetChallengeResultByIDDTO>> GetChallengeResultByChallengeId(int challengeId);
        Task<List<GetChallengeResultByIDDTO>> GetChallengeResultByUesrId(int UserId);
    }
}
