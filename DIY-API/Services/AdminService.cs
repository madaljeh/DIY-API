using DIY_API.DTOs.Challenge;
using DIY_API.Interfaces;
using DIY_API.Models;
using Microsoft.EntityFrameworkCore;
using static AdminStatisticsDTO;

namespace DIY_API.Services
{
    public class AdminService : IAdmin
    {
        private readonly DIYDbContext _diycontext;

        public AdminService(DIYDbContext context)
        {
            _diycontext = context;

        }
        public async Task<AdminStatisticsDTO> GetStatisticsAsync()
        {
            var statistics = new AdminStatisticsDTO
            {
                TotalUsers = await _diycontext.Users.CountAsync(),
                TotalChallenges = await _diycontext.Challenges.CountAsync(),
                TotalUserChallenges = await _diycontext.UserChallenges.CountAsync(),

                CompletedChallenges = await _diycontext.UserChallenges.CountAsync(x => x.Status == "Completed"),
                PendingChallenges = await _diycontext.UserChallenges.CountAsync(x => x.Status == "Pending"),
                ExpiredChallenges = await _diycontext.UserChallenges.CountAsync(x => x.Status == "Expired"),

                TopRatedChallenges = await _diycontext.Ratings
                    .GroupBy(r => r.ChallengeId)
                    .Select(g => new TopRatedChallengesDTO
                    {
                        ChallengesId = g.Key,
                        Title = g.First().Challenge.Title,
                        Description = g.First().Challenge.Description,
                        MediaUrl = g.First().Challenge.MediaUrl,
                        Level = g.First().Challenge.Level,
                        EstimatedDuration = g.First().Challenge.EstimatedDuration,
                        CategoryId = g.First().Challenge.CategoryId,
                        ChallengeRate = g.Average(r => r.Stars)
                    })
                    .OrderByDescending(x => x.ChallengeRate)
                    .Take(3)
                    .ToListAsync(),

                MostActiveUsers = await _diycontext.UserChallenges
                    .Where(x => x.Status == "Completed")
                    .GroupBy(x => x.UserId)
                    .Select(g => new MostActiveUserDTO
                    {
                        Username = g.First().User.Username,
                        Completed = g.Count()
                    })
                    .OrderByDescending(x => x.Completed)
                    .Take(5)
                    .ToListAsync()
            };

            return statistics;
        }
    }
}
