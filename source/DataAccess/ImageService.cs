using DomainModel.Models;
using SixLabors.ImageSharp.Formats.Jpeg;
namespace DataAccess;

public class ImageService
{
    private const int mediumSize = 1000;
    private const int smallSize = 300;

    private const string original = "original";
    private const string medium = "medium";
    private const string small = "small";
    //private const string large = "large";

    public async Task<string> SaveSingleImage(ImageInputModel image)
    {
        string name = string.Empty;
        await Task.Run(async () =>
        {
            using var imageResult = Image.Load(image.Content);
            name = $"{Guid.NewGuid()}.jpeg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\");

            CreateDirectories(path);

            await SaveImageInFileSystem(imageResult, name, path + original, imageResult.Width);
            await SaveImageInFileSystem(imageResult, name, path + medium, mediumSize);
            await SaveImageInFileSystem(imageResult, name, path + small, smallSize);
        });
        return await Task.FromResult(name);
    }

    private void CreateDirectories(string path)
    {
        Directory.CreateDirectory(path + original);
        Directory.CreateDirectory(path + medium);
        Directory.CreateDirectory(path + small);
    }

    private async Task SaveImageInFileSystem(Image image, string name, string path, int resizeWidth)
    {
        var width = image.Width;
        var height = image.Height;

        if (width > resizeWidth)
        {
            height = (int)((double)resizeWidth / width * height);
            width = resizeWidth;
        }

        image.Mutate(i => i.Resize(new Size(width, height)));
        image.Metadata.ExifProfile = null;

        await image.SaveAsJpegAsync($"{path}/{name}", new JpegEncoder
        {
            Quality = 75
        });
    }

}
