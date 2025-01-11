using CSHBlog.Web.Models.Domain;
using CSHBlog.Web.Models.ViewModels;
using CSHBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSHBlog.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminBlogPostsController : Controller
    {

        private ITagRepositories _tagRepo;
        private IBlogPostRepositories _blogRepo;

        public AdminBlogPostsController(ITagRepositories tagRepo, IBlogPostRepositories blogRepo)
        {
            _tagRepo = tagRepo;
            _blogRepo = blogRepo;
        }

        public async Task<IActionResult> Add()
        {
            //Display Tags for Selection
            var tags = await _tagRepo.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                //使用LINQ Select 轉換對象
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPostDomain = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishDate = addBlogPostRequest.PublishDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
            };

            List<Tag> Tags = new List<Tag>();

            //Map SelectedTags to Domina Tags
            foreach (string selected in addBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selected, out Guid id))
                {
                    var t = await _tagRepo.GetAsync(id);
                    if (t != null)
                    {
                        Tags.Add(t);
                    }
                }
            }

            blogPostDomain.Tags = Tags;

            await _blogRepo.AddAsync(blogPostDomain);

            //   return RedirectToAction("List");
            return RedirectToAction("Add");
        }


        public async Task<IActionResult> List()
        {
            var posts = await _blogRepo.GetAllAsync();
            return View(posts);
        }

        //Get Detail
        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await _blogRepo.GetAsync(id);
            var allTags = await _tagRepo.GetAllAsync();

            //Convert to ViewModel
            if (post != null)
            {
                var viewPost = new EditBlogPostRequest
                {
                    Id = post.Id,
                    Heading = post.Heading,
                    PageTitle = post.PageTitle,
                    Content = post.Content,
                    ShortDescription = post.ShortDescription,
                    FeaturedImageUrl = post.FeaturedImageUrl,
                    UrlHandle = post.UrlHandle,
                    PublishDate = post.PublishDate,
                    Author = post.Author,
                    Visible = post.Visible,
                    Tags = allTags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value =x.Id.ToString()                     

                    }),
                    SelectedTags = post.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(viewPost);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {       
           
            //Convert to ViewModel
            if (editBlogPostRequest != null)
            {
                var domainPost = new BlogPost
                {             
                    Id = editBlogPostRequest.Id,
                    Heading = editBlogPostRequest.Heading,
                    PageTitle = editBlogPostRequest.PageTitle,
                    Content = editBlogPostRequest.Content,
                    ShortDescription = editBlogPostRequest.ShortDescription,
                    FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                    UrlHandle = editBlogPostRequest.UrlHandle,
                    PublishDate = editBlogPostRequest.PublishDate,
                    Author = editBlogPostRequest.Author,
                    Visible = editBlogPostRequest.Visible,                   
                  
                };

                List<Tag> Tags = new List<Tag>();
                

                foreach (string selected in editBlogPostRequest.SelectedTags)
                {
                    if (Guid.TryParse(selected, out Guid id))
                    {
                        var t = await _tagRepo.GetAsync(id);
                        if (t != null)
                        {
                            Tags.Add(t);
                        }
                    }
                }

                domainPost.Tags = Tags;

                var updated = await _blogRepo.UpdateAsync(domainPost);

                if (updated != null)
                {
                    //return View(domainPost);
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("List");
                }
               

            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> ListDelete(Guid id)
        {
           var existing = await _blogRepo.DeleteAsync(id);
            return RedirectToAction("List");


        }


    }
}
