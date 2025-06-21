using DIY_API.DTOs.Rating;
using DIY_API.Interfaces;
using DIY_API.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DIY_API.Services
{
    public class RataingServices : IRatings
    {
        private readonly DIYDbContext _diycontext;

        public RataingServices(DIYDbContext context)
        {
            _diycontext = context;

        }
        public async Task<string> CreateRate(CreateRateDTO input)
        {
            Rating rate = new Rating();

            rate.ChallengeId = input.ChallengeId;
            rate.UserId = input.UserId;
            rate.Stars = input.Stars;
            rate.Message = input.Message;
            rate.RatedAt = DateTime.Now;
            _diycontext.Ratings.Add(rate);
            await _diycontext.SaveChangesAsync();

            return "Rating created successfully.";
        }

        public async Task<List<GetRateDTO>> GetRateByCallaengeID(int id)
        {
            var ratings = await _diycontext.Ratings
                .Where(r => r.ChallengeId == id)
                .Select(r => new GetRateDTO
                {
                    UserId = r.UserId,
                    ChallengeId = r.ChallengeId,
                    Stars = r.Stars,
                    Message = r.Message,
                    RatedAt = r.RatedAt
                }).ToListAsync();
            return ratings;
        }

        public async Task<List<GetRateDTO>> GetRateByUserID(int id)
        {
            var ratings = await _diycontext.Ratings
                .Where(r => r.UserId == id)
                .Select(r => new GetRateDTO
                {
                    UserId = r.UserId,
                    ChallengeId = r.ChallengeId,
                    Stars = r.Stars,
                    Message = r.Message,
                    RatedAt = r.RatedAt
                }).ToListAsync();
            return ratings;
        }
    }
}
