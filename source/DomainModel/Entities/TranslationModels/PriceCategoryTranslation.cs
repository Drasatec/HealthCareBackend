using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class PriceCategoryTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? PriceCategoryId { get; set; }

    public string? Description { get; set; }

    public string LangCode { get; set; } = null!;

    //public virtual Language? LangCodeNavigation { get; set; }
    [JsonIgnore]
    public virtual PriceCategory? PriceCategory { get; set; }
}
