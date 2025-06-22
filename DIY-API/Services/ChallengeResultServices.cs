using DIY_API.DTOs.ChallengeResultDTO;
using DIY_API.Interfaces;
using DIY_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DIY_API.Services
{
    public class ChallengeResultServices : IChallengeResult
    {
        private readonly DIYDbContext _diycontext;

        public ChallengeResultServices(DIYDbContext context)
        {
            _diycontext = context;

        }
        public async Task<List<GetChallengeResultDTO>> GetAllChallengeResult()
        {
            var resultes = await (from ChallengeResults in _diycontext.ChallengeResults
                                  join UserChallenges in _diycontext.UserChallenges on                      ChallengeResults.UserChallengeId equals UserChallenges.UserChallengeId
                                  join Challenges in _diycontext.Challenges on UserChallenges.ChallengeId equals        Challenges.ChallengesId
                                  where Challenges.IsActive == true && UserChallenges.IsActive == true
                                  select new GetChallengeResultDTO
                                  {
                                      ChallengeResultId = ChallengeResults.ChallengeResultId,
                                      UserChallengeId = ChallengeResults.UserChallengeId,
                                      Title = Challenges.Title,
                                      FileUrl = ChallengeResults.FileUrl,
                                      UploadedAt = ChallengeResults.UploadedAt
                                  }).ToListAsync();
            return resultes;
        }

        public async Task<List<GetChallengeResultByIDDTO?>> GetChallengeResultByUesrId(int UserId)
        {
            var resulte = from ChallengeResults in _diycontext.ChallengeResults
                          join UserChallenges in _diycontext.UserChallenges on ChallengeResults.UserChallengeId equals UserChallenges.UserChallengeId
                          join Challenges in _diycontext.Challenges on UserChallenges.ChallengeId equals Challenges.ChallengesId
                          join Users in _diycontext.Users on UserChallenges.UserId equals Users.UserId
                          where Users.UserId == UserId && Challenges.IsActive == true && UserChallenges.IsActive == true
                          select new GetChallengeResultByIDDTO
                          {
                              Title = Challenges.Title,
                              Username = Users.Username,
                              FileUrl = ChallengeResults.FileUrl,
                              UploadedAt = ChallengeResults.UploadedAt
                          };
            return await resulte.ToListAsync();
        }

        public async Task<List<GetChallengeResultByIDDTO>> GetChallengeResultByChallengeId(int challengeId)
        {
            var resulte = from ChallengeResults in _diycontext.ChallengeResults
                         join UserChallenges in _diycontext.UserChallenges on ChallengeResults.UserChallengeId equals UserChallenges.UserChallengeId
                         join Challenges in _diycontext.Challenges on UserChallenges.ChallengeId equals Challenges.ChallengesId
                          join Users in _diycontext.Users on UserChallenges.UserId equals Users.UserId
                          where Challenges.ChallengesId == challengeId && Challenges.IsActive == true && UserChallenges.IsActive == true
                         select new GetChallengeResultByIDDTO
                         {
                             Title = Challenges.Title,
                             Username = Users.Username,
                             FileUrl = ChallengeResults.FileUrl,
                             UploadedAt = ChallengeResults.UploadedAt
                         };
            return await resulte.ToListAsync();

        }
    }
}
