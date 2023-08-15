using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class PromotionsTranslation
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? PromotionId { get; set; }

    public string LangCode { get; set; } = null!;

    //public virtual Language? LangCodeNavigation { get; set; }

    [JsonIgnore]
    public virtual Promotion? Promotion { get; set; }
}
