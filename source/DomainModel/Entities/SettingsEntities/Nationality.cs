using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class Nationality
{
    public int Id { get; set; }

    public string? Symbol { get; set; }

    public virtual ICollection<NationalitiesTranslation> NationalitiesTranslations { get; set; } = new List<NationalitiesTranslation>();
}
