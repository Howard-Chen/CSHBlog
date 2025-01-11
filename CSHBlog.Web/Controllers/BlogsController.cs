using CSHBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CSHBlog.Web.Controllers
{
    public class BlogsController : Controller
    {

        private IBlogPostRepositories _repo;

        public BlogsController(IBlogPostRepositories repo)
        {
            _repo = repo;
        }

        [Route("Blogs/{handle}")]
        public async Task<IActionResult> Index(string handle)
        {
            var blog = await _repo.GetByUrlAsync(handle);


            return View(blog);
        }
    }
}
