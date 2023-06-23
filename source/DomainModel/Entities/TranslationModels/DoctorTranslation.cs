using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class DoctorTranslation
{
    public int Id { get; set; }

    public string FullName { get; set; }  = string.Empty;

    public string? Headline { get; set; }

    public string? About { get; set; }

    public int? DoctorId { get; set; }

    public string LangCode { get; set; } = null!;
    [JsonIgnore]
    public virtual Doctor? Doctor { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
