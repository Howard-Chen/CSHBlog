using CSHBlog.Web.Data;
using CSHBlog.Web.Models.Domain;
using CSHBlog.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CSHBlog.Web.Repositories
{
    public class TagRepositories : ITagRepositories
    {

        private CSHDbContext _dbContext;

        public TagRepositories(CSHDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {

            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {

            var existing = _dbContext.Tags.Find(id);

            if (existing != null)
            {
                _dbContext.Tags.Remove(existing); //Remove是Que所以不用Async
                await _dbContext.SaveChangesAsync();

                return existing;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {

            var tags = await _dbContext.Tags.ToListAsync();
            return tags;
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            return tag;
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existing = await _dbContext.Tags.FindAsync(tag.Id);

            if (existing != null)
            {
                existing.Name = tag.Name;
                existing.DisplayName = tag.DisplayName;
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
