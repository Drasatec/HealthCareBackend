namespace DomainModel.Entities.TranslationModels;

public partial class MaritalStatusTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public byte? MaritalId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual MaritalStatus? Marital { get; set; }
}
