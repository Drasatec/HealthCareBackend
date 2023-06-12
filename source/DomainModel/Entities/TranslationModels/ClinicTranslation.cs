using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class ClinicTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? ClinicId { get; set; }

    public string LangCode { get; set; } = null!;
    [JsonIgnore]
    public virtual Clinic? Clinic { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
