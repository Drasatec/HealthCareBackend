using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class PriceCategory
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public DateTime? CreateOn { get; set; }

    public string? Symbol { get; set; }

    public virtual ICollection<PriceCategoryTranslation> PriceCategoryTranslations { get; set; } = new List<PriceCategoryTranslation>();
}
