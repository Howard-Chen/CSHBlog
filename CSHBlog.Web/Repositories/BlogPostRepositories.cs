using Azure;
using CSHBlog.Web.Data;
using CSHBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSHBlog.Web.Repositories
{
    public class BlogPostRepositories : IBlogPostRepositories
    {

        private CSHDbContext _dbContext;

        public BlogPostRepositories(CSHDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existing = _dbContext.BlogPosts.Find(id);
            if (existing != null)
            {
                _dbContext.BlogPosts.Remove(existing); //Remove是Que所以不用Async
                await _dbContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var blogPosts = await _dbContext.BlogPosts.Include(x => x.Tags).OrderBy(x => x.PublishDate).ToListAsync();
            return blogPosts;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {

            var blogPost = await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);

            return blogPost;
        }

        public async Task<BlogPost?> GetByUrlAsync(string urlHandle)
        {
            var blogPost = await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

            return blogPost;
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existing = await _dbContext.BlogPosts.Include(x=> x.Tags).FirstOrDefaultAsync(x =>x.Id == blogPost.Id);
            if (existing != null)
            {
                existing.Heading = blogPost.Heading;
                existing.PageTitle = blogPost.PageTitle;
                existing.Content = blogPost.Content;
                existing.ShortDescription = blogPost.ShortDescription;
                existing.Author = blogPost.Author;
                existing.UrlHandle = blogPost.UrlHandle;
                existing.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existing.PublishDate = blogPost.PublishDate;
                existing.Visible = blogPost.Visible;
                existing.Tags = blogPost.Tags;

                await _dbContext.SaveChangesAsync();
                return existing;
            }
            else
            {
                return null;
            }


        }
    }
}
