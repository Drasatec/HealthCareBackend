using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class PriceCategoryTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? PriceCategoryId { get; set; }

    public string? Description { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual PriceCategory? PriceCategory { get; set; }
}
