using CSHBlog.Web.Models.Domain;

namespace CSHBlog.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }



    }
}
