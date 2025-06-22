using DIY_API.DTOs.UserChallenge;
using DIY_API.Interfaces;
using DIY_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DIY_API.Services
{
    public class UserChallengeService : IUserChallenge
    {
        private readonly DIYDbContext _diycontext;

        public UserChallengeService(DIYDbContext context)
        {
            _diycontext = context;

        }

        public async Task<List<UserChallengeDTO>> GetUserChallengeDetails(int userId)
        {
            var result = await _diycontext.UserChallenges
                .Where(uc => uc.UserId == userId)
                .Join(_diycontext.Challenges,
                      uc => uc.ChallengeId,
                      c => c.ChallengesId,
                      (uc, c) => new
                      {
                          UserChallenge = uc,
                          Challenge = c
                      })
                .ToListAsync();

            if (result == null || !result.Any()) return null;

            var userChallengeDetails = result.Select(item =>
            {
                var dueDate = item.UserChallenge.StartDate.AddMinutes(item.Challenge.EstimatedDuration ?? 0);

                // Determine dynamic status
                string status = DateTime.Now > dueDate ? "Expired" : "Pending";

                return new UserChallengeDTO
                {
                    UserId = item.UserChallenge.UserId,
                    ChallengeId = item.UserChallenge.ChallengeId,
                    StartDate = item.UserChallenge.StartDate,
                    DueDate = dueDate,
                    Status = status,
                    CreationDate = item.UserChallenge.CreationDate,
                    ChallengeTitle = item.Challenge.Title,
                    ChallengeDescription = item.Challenge.Description,
                    EstimatedDuration = item.Challenge.EstimatedDuration,
                };
            }).ToList();

            return userChallengeDetails;
        }

        public async Task<bool> UpdateUserChallengeStatus(int userId, int challengeId, UpdateUserChallengeStatusDTO input)
        {
            var userChallenge = await _diycontext.UserChallenges
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);
            if (userChallenge == null)
            {
                return false; 
            }
            userChallenge.Status = input.Status;
            _diycontext.UserChallenges.Update(userChallenge);
            try
            {
                await _diycontext.SaveChangesAsync();
                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user challenge status.", ex);
            }
        }


        public async Task<string> AddUserChallenge(int userId, int challengeId)
        {
            var user = await _diycontext.Users.FindAsync(userId);
            var challenge = await _diycontext.Challenges.FindAsync(challengeId);

            if (user == null || challenge == null || challenge.IsActive == false)
                return "User or Challenge not found or not active.";

            var existing = await _diycontext.UserChallenges
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);

            if (existing != null && existing.Status != "Expired")
            {
                return "User already has this challenge in progress or completed.";
            }

            var startDate = DateTime.Now;
            var dueDate = startDate.AddMinutes(challenge.EstimatedDuration ?? 0);
            string status = DateTime.Now > dueDate ? "Expired" : "Pending";

            var userChallenge = new UserChallenge
            {
                UserId = userId,
                ChallengeId = challengeId,
                StartDate = startDate,
                DueDate = dueDate,
                Status = status,
                IsActive = true,
                CreationDate = startDate
            };

            _diycontext.UserChallenges.Add(userChallenge);
            await _diycontext.SaveChangesAsync();

            return "Challenge assigned to user successfully.";
        }


    }
}
