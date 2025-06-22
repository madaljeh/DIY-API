using DIY_API.DTOs.UserChallenge;
using DIY_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChallengeController : ControllerBase
    {
        private readonly IUserChallenge _appService;

        public UserChallengeController(IUserChallenge appService)
        {
            _appService = appService;

        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserChallengeDetails(int userId)
        {
            try
            {
                var userChallenge = await _appService.GetUserChallengeDetails(userId);
                if (userChallenge == null)
                {
                    return NotFound("User challenge not found.");
                }
                return Ok(userChallenge);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUserChallengeStatus(int userId, int challengeId, [FromBody] UpdateUserChallengeStatusDTO input)
        {
            if (input == null || string.IsNullOrEmpty(input.Status))
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var result = await _appService.UpdateUserChallengeStatus(userId, challengeId, input);
                if (result)
                {
                    return Ok("User challenge status updated successfully.");
                }
                else
                {
                    return NotFound("User challenge not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserChallenge(int userId, int challengeId)
        {
            try
            {
                var result = await _appService.AddUserChallenge(userId, challengeId);
                if (result == "Challenge assigned to user successfully.")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

    }

}
