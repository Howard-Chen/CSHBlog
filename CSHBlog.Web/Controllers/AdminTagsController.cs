using CSHBlog.Web.Models.Domain;
using CSHBlog.Web.Models.ViewModels;
using CSHBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CSHBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        //https://localhost:5267/AdminTags/Add

        private ITagRepositories _repo;

        public AdminTagsController(ITagRepositories repo)
        {
            _repo = repo;
        }

        [HttpGet] //default is get
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //使用DomainModel的class建立實例，Data值從 ViewModel addTagRequest 取得
            var tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await _repo.AddAsync(tag);
            return RedirectToAction("List");
            //return View("Add"); //不同名的view需指定
        }

        public async Task<IActionResult> List()
        {
            var tags = await _repo.GetAllAsync();
            return View(tags); //同名的View可以右鍵創立
        }


        public async Task<IActionResult> Edit(Guid id) //GetDetail
        {
            //var tag = _dbContext.Tags.Find(id);
            var tag = await _repo.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            //var tag = _dbContext.Tags.Find(id);
            var tag = new Tag() //convert to DomainModel
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updated = await _repo.UpdateAsync(tag);

            if (updated != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var existing = await _repo.DeleteAsync(editTagRequest.Id);

            if (existing != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ListDelete(Guid id)
        {
            var existing = await _repo.DeleteAsync(id);
            return RedirectToAction("List");
        }
    }
}
