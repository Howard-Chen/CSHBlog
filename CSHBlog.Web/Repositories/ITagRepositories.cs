using CSHBlog.Web.Models.Domain;

namespace CSHBlog.Web.Repositories

{
    public interface ITagRepositories
    {
        public Task<IEnumerable<Tag>> GetAllAsync();

        public Task<Tag?> GetAsync(Guid id);

        public Task<Tag> AddAsync(Tag tag);

        public Task<Tag?> UpdateAsync(Tag tag);

        public Task<Tag?> DeleteAsync(Guid id);
    }
}
