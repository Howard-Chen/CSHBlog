using CSHBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CSHBlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageRepositories _imageRepo;
        public ImagesController(IImageRepositories imageRepo)
        {
            _imageRepo = imageRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return Ok("Get Success");
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            string url = await _imageRepo.UploadAsync(file);
            if(url == null)
            {
                return Problem("Error", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = url });
        }
    }
}
