using CSHBlog.Web.Models;
using CSHBlog.Web.Models.ViewModels;
using CSHBlog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace CSHBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBlogPostRepositories _blogsrepo;
        private ITagRepositories _tagsrepo;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepositories blogsrepo, ITagRepositories tagsrepo)
        {
            _logger = logger;
            _blogsrepo = blogsrepo;
            _tagsrepo = tagsrepo;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogsrepo.GetAllAsync();
            var tags = await _tagsrepo.GetAllAsync();


            //Convert to ViewModel
            if (blogs != null)
            {
                var homeView = new HomeViewModel
                {
                    BlogPosts = blogs,
                    Tags = tags
                };

                return View(homeView);
            }

            return View(null);



        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
