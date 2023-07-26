using DomainModel.Helpers;
using DomainModel.Models;
using SixLabors.ImageSharp.Formats.Jpeg;
namespace DataAccess;

public static class DataAccessImageService
{
    private const int mediumSize = 1000;
    private const int smallSize = 300;

    private const string original = "original";
    private const string medium = "medium";
    private const string small = "small";
    //private const string large = "large";
    private static readonly string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\");

    public static async Task<string> SaveSingleImage(Stream image)
    {
        if (image != null)
        {
            var imageName = Helper.GenerateImageName();
            _ = SaveSingleImage(image, imageName);
            return imageName;
        }
        return null;
    }

    public static async Task<string> SaveSingleImage(Stream image, string? name = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            name = Helper.GenerateImageName();
        }
        using var imageResult = Image.Load(image);
        CreateDirectories(path);

        // await SaveImageInFileSystem(imageResult, name, path + original, imageResult.Width);
        await SaveImageInFileSystem(imageResult, name, path + medium, mediumSize);
        await SaveImageInFileSystem(imageResult, name, path + small, smallSize);

        return await Task.FromResult(name);
    }

    public static async Task<string> UpdateSingleImage(Stream image, string name)
    {
        RemoveImage(name);
        using var imageResult = Image.Load(image);

        //await SaveImageInFileSystem(imageResult, name, path + original, imageResult.Width);
        await SaveImageInFileSystem(imageResult, name, path + medium, mediumSize);
        await SaveImageInFileSystem(imageResult, name, path + small, smallSize);
        return await Task.FromResult(name);
    }

    private static void CreateDirectories(string path)
    {
        Directory.CreateDirectory(path + original);
        Directory.CreateDirectory(path + medium);
        Directory.CreateDirectory(path + small);
    }
    private static void RemoveImage(string name)
    {
        File.Delete(path + "original\\" + name);
        File.Delete(path + "medium\\" + name);
        File.Delete(path + "small\\" + name);
    }

    private static async Task SaveImageInFileSystem(Image image, string name, string path, int resizeWidth)
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
