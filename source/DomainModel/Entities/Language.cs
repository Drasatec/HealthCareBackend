namespace DomainModel.Entities;

public partial class Language
{
    public string Code { get; set; } = null!;

    public string? LanguageName { get; set; }

    public bool IsDefault { get; set; }
}
