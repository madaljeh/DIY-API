using DIY_API.DTOs.Challenge;
using DIY_API.DTOs.User;

namespace DIY_API.Interfaces
{
    public interface IChallenge
    {
        Task<List<GetAllChallengesDTO>> GetAllChallenges();
        Task<GetAllChallengesDTO> GetChallengesById(int challangeId);
        Task<List<GetAllChallengesDTO>> GetChallengesByCategoryId(int categoryId);
        Task<string> CreateChallenge(CreateChallengeDTO input);
        Task<string> UpdateChallenges(int challangeId, UpdateChallengesDTO input);
        Task<bool> DeleteChallenges(int challangeId);
        Task<bool> ChallengeActivationStatus(int challangeId, ChallengeActivationDTO input);
        Task<List<TopRatedChallengesDTO>> GetTopRatedChallenges();
        Task<List<TopRecommendedChallengesDTO>> GetTopRecommendedChallenges();


    }

}
