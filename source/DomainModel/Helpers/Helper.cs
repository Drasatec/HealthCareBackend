namespace DomainModel.Helpers;

public static class Helper
{
    public static string GenerateImageName()
    {
        return $"{Guid.NewGuid()}.jpeg";
    }
    
    public static int SkipValue(int page, int pageSize )
    {
        return  (page - 1) * pageSize;
    }
}
