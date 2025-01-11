using CSHBlog.Web.Models.Domain;

namespace CSHBlog.Web.Repositories
{
    public interface IBlogPostRepositories
    {

        public Task<BlogPost?> GetAsync(Guid id);

        public Task<BlogPost?> GetByUrlAsync(string urlHandle);
        public Task<IEnumerable<BlogPost>> GetAllAsync();
        public Task<BlogPost> AddAsync(BlogPost blogPost);
        public Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        public Task<BlogPost?> DeleteAsync(Guid id);


    }
}
