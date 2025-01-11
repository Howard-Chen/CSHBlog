
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;

namespace CSHBlog.Web.Repositories
{
    public class CloudinaryImageRepositories : IImageRepositories
    {

        public async Task<string> UploadAsync(IFormFile file)
        {
            //DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            var client = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            client.Api.Secure = true;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Console.WriteLine(uploadResult.JsonObj);
                return uploadResult.SecureUrl.ToString();
            }

            return null;

        }
    }
}
