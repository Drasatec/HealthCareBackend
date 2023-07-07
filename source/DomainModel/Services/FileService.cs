using DomainModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DomainModel.Services;

public static class FileService
{
    private static readonly string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Files\\");

    public static async Task<string> SaveSingleFile(Stream file, string? name = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            name = Helper.GenerateImageName();
        }

        var filePath = Path.Combine(path, name);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return await Task.FromResult(name);
    }


    public static async Task<string> UpdateSingleFile(Stream file, string name)
    {
        RemoveFile(name);

        await SaveSingleFile(file, name);

        return await Task.FromResult(name);
    }

    public static void RemoveFile(string name)
    {
        File.Delete(Path.Combine(path, name));
    }
}
