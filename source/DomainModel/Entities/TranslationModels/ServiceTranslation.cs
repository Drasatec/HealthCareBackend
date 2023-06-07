using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class ServiceTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ServiceId { get; set; }

    public string? LangCode { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
    [JsonIgnore]
    public virtual Service? Service { get; set; }
}
