using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class MainServiceTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? MainServiceId { get; set; }

    public string LangCode { get; set; } = null!;
    [JsonIgnore]
    public virtual MainService? MainService { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
