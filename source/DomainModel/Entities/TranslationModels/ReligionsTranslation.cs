namespace DomainModel.Entities.TranslationModels;

public partial class ReligionsTranslation
{
    public byte Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public byte? ReligionId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual Religion? Religion { get; set; }
}
