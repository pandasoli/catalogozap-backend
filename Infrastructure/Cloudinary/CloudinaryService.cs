using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;

namespace CatalogoZap.Infrastructure.CloudinaryService;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile image);
    Task<DeletionResult> DeleteImageAsync(string path);
}
public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {
        DotEnv.Load();
        var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

        if (string.IsNullOrEmpty(cloudinaryUrl))
            throw new Exception("CLOUDINARY_URL not found");

        _cloudinary = new Cloudinary(cloudinaryUrl);
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        using var stream = image.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(Guid.NewGuid().ToString(), stream),
            Folder = "products"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);
        return result.SecureUrl.ToString();
    }

    public async Task<DeletionResult> DeleteImageAsync(string path)
    {
        var deletionParams = new DeletionParams(path)
        {
            ResourceType = ResourceType.Image
        };

        var result = await _cloudinary.DestroyAsync(deletionParams);

        return result;

    }

}
