using DIY_API.DTOs.Rating;
using DIY_API.Helper;
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

        public async Task<bool> DeleteRate(int Id)
        {
            var rate = await _diycontext.Ratings.FindAsync(Id);
            if (rate == null)
            {
                return false;
            }
            _diycontext.Ratings.Remove(rate);
            await _diycontext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetRateDTO>> GetAllRate(PaginationParameters pagination)
        {
            return await (from rate in _diycontext.Ratings
                          join user in _diycontext.Users on rate.UserId equals user.UserId
                          select new GetRateDTO
                          {
                              UserId = rate.UserId,
                              Username = user.Username,
                              ChallengeId = rate.ChallengeId,
                              Stars = rate.Stars,
                              Message = rate.Message,
                              RatedAt = rate.RatedAt
                          } 
                          ).ToListAsync();
        }

        public async Task<List<GetRateDTO>> GetRateByCallaengeID(int id)
        {
            return await (from rate in _diycontext.Ratings
                          join user in _diycontext.Users on rate.UserId equals user.UserId
                          join challenge in _diycontext.Challenges on rate.ChallengeId equals challenge.ChallengesId
                          where rate.ChallengeId == id
                          select new GetRateDTO
                          {
                              UserId = rate.UserId,
                              Username = user.Username,
                              Title = challenge.Title,
                              ChallengeId = rate.ChallengeId,
                              Stars = rate.Stars,
                              Message = rate.Message,
                              RatedAt = rate.RatedAt
                          }
                           ).ToListAsync();
        }

        public async Task<List<GetRateDTO>> GetRateByUserID(int id)
        {
            return await (from rate in _diycontext.Ratings
                          join user in _diycontext.Users on rate.UserId equals user.UserId
                          join challenge in _diycontext.Challenges on rate.ChallengeId equals challenge.ChallengesId
                          where rate.UserId == id
                          select new GetRateDTO
                          {
                              UserId = rate.UserId,
                              Username = user.Username,
                              Title = challenge.Title,
                              ChallengeId = rate.ChallengeId,
                              Stars = rate.Stars,
                              Message = rate.Message,
                              RatedAt = rate.RatedAt
                          }
                           ).ToListAsync();


        }

        public async Task<bool> RateActivationStatus(int RateId, RateActivationDTO input)
        {
            var rate = await _diycontext.Ratings.FindAsync(RateId);
            if (rate == null)
            {
                throw new KeyNotFoundException($"Rate with ID {RateId} not found.");
            }
            rate.IsActive = input.IsActive;
            _diycontext.Ratings.Update(rate);
            await _diycontext.SaveChangesAsync();
            return true;
        }
    }
}
