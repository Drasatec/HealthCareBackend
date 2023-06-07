using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class NationalitiesTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NationalityId { get; set; }

    public string? LangCode { get; set; }
    [JsonIgnore]
    public virtual Nationality? Nationality { get; set; }
    //public virtual Language? LangCodeNavigation { get; set; }

}
