using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class HospitalFeatureTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int FeatureId { get; set; }

    public string LangCode { get; set; } = null!;
    
    [JsonIgnore]
    public virtual HospitalFeature? Feature { get; set; }

   // public virtual Language? LangCodeNavigation { get; set; }
}
