namespace CSHBlog.Web.Repositories
{
    public interface IImageRepositories
    {

        public Task<string> UploadAsync(IFormFile file);
      

    }
}
