using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class Promotion
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public int? Position { get; set; }

    public string? Link { get; set; }

    public virtual ICollection<PromotionsTranslation> PromotionsTranslations { get; set; } = new List<PromotionsTranslation>();
}
