using DIY_API.DTOs.Authantication;
using DIY_API.DTOs.Category;
using DIY_API.Helper;
using DIY_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategories _appService;

        public CategoryController(ICategories appService)
        {
            _appService = appService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCategory([FromQuery] PaginationParameters pagination)
        {
            try
            {
                var response = await _appService.GetAllCategory(pagination);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            try
            {
                var response = await _appService.GetCategoryById(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddNewCategory(NewCategoryDTO input)
        {
            try
            {
                var response = await _appService.AddNewCategory(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO input)
        {
            try
            {
                var response = await _appService.UpdateCategory(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            try
            {
                var response = await _appService.DeleteCategory(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
