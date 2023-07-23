using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class TypesVisitTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? TypeVisitId { get; set; }

    public string LangCode { get; set; } = null!;

    //public virtual Language? LangCodeNavigation { get; set; }
    [JsonIgnore]
    public virtual TypesVisit? TypeVisit { get; set; }
}
