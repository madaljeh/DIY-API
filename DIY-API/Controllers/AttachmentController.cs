using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new Exception("Please Enter Valid File");
                }
                //generate new file Name extracted from current file name 
                //string newFileName = DateTime.Now.ToString()+"-"+file.FileName;
                string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string directory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                using (var fs = new FileStream(Path.Combine(directory, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                return Ok($"/uploads/{fileName}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
