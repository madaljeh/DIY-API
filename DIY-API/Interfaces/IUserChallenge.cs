using DIY_API.DTOs.Challenge;
using DIY_API.DTOs.UserChallenge;

namespace DIY_API.Interfaces
{
    public interface IUserChallenge
    {
        Task<string> AddUserChallenge(int userId, int challengeId);
       
        Task<bool> UpdateUserChallengeStatus(int userId, int challengeId, UpdateUserChallengeStatusDTO input);
        Task<List<UserChallengeDTO>> GetUserChallengeDetails(int userId);
    }
}
