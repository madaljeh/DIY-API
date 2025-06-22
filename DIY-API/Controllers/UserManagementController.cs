using DIY_API.DTOs.Authantication;
using DIY_API.DTOs.User;
using DIY_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagement _appService;

        public UserManagementController(IUserManagement appService)
        {
            _appService = appService;

        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _appService.GetAllUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var response = await _appService.GetUserById(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAdmin(CreateAdminDTO input)
        {
            try
            {
                var response = await _appService.CreateAdmin(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UserActivationStatus(int userId, [FromBody] UserActivationDTO input)
        {
            try
            {
                var response = await _appService.UserActivationStatus(userId, input);
                if (response)
                {
                    return Ok("User activation status updated successfully");
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserInfo(int userId, [FromBody] UpdateUserInfoDTO input)
        {
            try
            {
                var response = await _appService.UpdateUserInfo(userId, input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var response = await _appService.DeleteUser(userId);
                if (response)
                {
                    return Ok("User deleted successfully");
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
       
    }
}
