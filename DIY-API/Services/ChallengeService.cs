using DIY_API.DTOs.Challenge;
using DIY_API.Interfaces;
using DIY_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DIY_API.Services
{
    public class ChallengeService : IChallenge
    {
        private readonly DIYDbContext _diycontext;

        public ChallengeService(DIYDbContext context)
        {
            _diycontext = context;

        }

        public async Task<bool> ChallengeActivationStatus(int challangeId, ChallengeActivationDTO input)
        {
            var challenge = await _diycontext.Challenges.FindAsync(challangeId);
            if (challenge == null)
            {
                throw new KeyNotFoundException($"Challenge with ID {challangeId} not found.");
            }
            challenge.IsActive = input.IsActive;
            _diycontext.Challenges.Update(challenge);
            await _diycontext.SaveChangesAsync();
            return true;

        }

        public async Task<string> CreateChallenge(CreateChallengeDTO input)
        {
            var challenge = new Challenge
            {
                Title = input.Title,
                Description = input.Description,
                MediaUrl = input.MediaUrl,
                Level = input.Level,
                EstimatedDuration = input.EstimatedDuration,
                CategoryId = input.CategoryId,
                CreatedBy = input.CreatedBy,
                CreationDate = input.CreationDate ?? DateTime.UtcNow,
                IsActive = input.IsActive
            };
            _diycontext.Challenges.Add(challenge);
            await _diycontext.SaveChangesAsync();
            return "Challenge created successfully";

        }

        public async Task<bool> DeleteChallenges(int challangeId)
        {
           var challenge = await _diycontext.Challenges.FindAsync(challangeId);
            if (challenge == null)
            {
                return false; 
            }
            _diycontext.Challenges.Remove(challenge);
            await _diycontext.SaveChangesAsync();
            return true; 
        }

        public async Task<List<GetAllChallengesDTO>> GetAllChallenges()
        {
           var challenges = _diycontext.Challenges.ToList();
            var challengeList = challenges.Select(challenge => new GetAllChallengesDTO
            {
                ChallengesId = challenge.ChallengesId,
                Title = challenge.Title,
                Description = challenge.Description,
                MediaUrl = challenge.MediaUrl,
                Level = challenge.Level,
                EstimatedDuration = challenge.EstimatedDuration,
                CategoryId = challenge.CategoryId,
                CreationDate = challenge.CreationDate


            }).ToList();
            return  challengeList;

        }

        public async Task<List<GetAllChallengesDTO>> GetChallengesByCategoryId(int categoryId)
        {
           var challenges = await _diycontext.Challenges
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
            if (challenges == null || challenges.Count == 0)
            {
                throw new KeyNotFoundException($"No challenges found for category ID {categoryId}.");
            }
            return challenges.Select(challenge => new GetAllChallengesDTO
            {
                ChallengesId = challenge.ChallengesId,
                Title = challenge.Title,
                Description = challenge.Description,
                MediaUrl = challenge.MediaUrl,
                Level = challenge.Level,
                EstimatedDuration = challenge.EstimatedDuration,
                CategoryId = challenge.CategoryId,
                CreationDate = challenge.CreationDate


            }).ToList();
        }

        public async Task<GetAllChallengesDTO> GetChallengesById(int challangeId)
        {
            var challenge = await _diycontext.Challenges.FindAsync(challangeId);
            if (challenge == null)
            {
                throw new KeyNotFoundException($"Challenge with ID {challangeId} not found.");
            }
            return new GetAllChallengesDTO
            {
                ChallengesId = challenge.ChallengesId,
                Title = challenge.Title,
                Description = challenge.Description,
                MediaUrl = challenge.MediaUrl,
                Level = challenge.Level,
                EstimatedDuration = challenge.EstimatedDuration,
                CategoryId = challenge.CategoryId,
                CreationDate = challenge.CreationDate
            };


        }

        public async Task<List<TopRatedChallengesDTO>> GetTopRatedChallenges()
        {

            var topRatedChallenges = await _diycontext.Ratings.Where(c => c.IsActive == true)
     .GroupBy(r => r.ChallengeId)
     .Select(g => new
     {
         ChallengeId = g.Key,
         ChallengeRate = g.Average(r => r.Stars) 
     })
     .OrderByDescending(c => c.ChallengeRate) 
     .Take(8) 
     .Join(_diycontext.Challenges,
           rating => rating.ChallengeId, 
           challenge => challenge.ChallengesId, 
           (rating, challenge) => new TopRatedChallengesDTO
           {
               ChallengesId = challenge.ChallengesId,
               Title = challenge.Title,
               Description = challenge.Description,
               MediaUrl = challenge.MediaUrl,
               Level = challenge.Level,
               EstimatedDuration = challenge.EstimatedDuration,
               CategoryId = challenge.CategoryId,
               ChallengeRate = rating.ChallengeRate
           })
     .ToListAsync();

            return topRatedChallenges;

        }

        public async Task<List<TopRecommendedChallengesDTO>> GetTopRecommendedChallenges()
        {
            var topRecommendedChallenges = await _diycontext.Challenges
        .Where(c => c.IsActive == true)
        .OrderByDescending(c => c.CreationDate) 
        .Take(3) 
        .Select(c => new TopRecommendedChallengesDTO
        {
            ChallengesId = c.ChallengesId,
            Title = c.Title,
            Description = c.Description,
            MediaUrl = c.MediaUrl,
            Level = c.Level,
            EstimatedDuration = c.EstimatedDuration,
            CategoryId = c.CategoryId
        })
        .ToListAsync();

            return topRecommendedChallenges;
        }
        public async Task<string> UpdateChallenges(int challangeId, UpdateChallengesDTO input)
        {
            var challenge = await _diycontext.Challenges.FindAsync(challangeId);
            if (challenge == null)
            {
                throw new KeyNotFoundException($"Challenge with ID {challangeId} not found.");
            }
            challenge.Title = input.Title ?? challenge.Title;
            challenge.Description = input.Description ?? challenge.Description;
            challenge.MediaUrl = input.MediaUrl ?? challenge.MediaUrl;
            challenge.Level = input.Level ?? challenge.Level;
            challenge.EstimatedDuration = input.EstimatedDuration ?? challenge.EstimatedDuration;
            challenge.CategoryId = input.CategoryId;
            challenge.UpdatedBy = input.UpdatedBy ?? challenge.UpdatedBy;
            challenge.IsActive = input.IsActive ?? challenge.IsActive;
            challenge.UpdateDate = input.UpdateDate ?? DateTime.UtcNow;
            _diycontext.Challenges.Update(challenge);
            await _diycontext.SaveChangesAsync();
            return "Challenge updated successfully";

        }
    }
}
