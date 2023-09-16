namespace DomainModel.Helpers;

public static class Helper
{
    public static string GenerateImageName()
    {
        return $"{Guid.NewGuid()}.jpeg";
    }
    
    public static string GenerateFileName(string fileName)
    {
        var extension = Path.GetExtension(fileName);

        return $"{Guid.NewGuid()}{extension}";
    }

    public static string VerificationCode()
    {
        return new Random().Next(100000, 999999).ToString();
    }

}
