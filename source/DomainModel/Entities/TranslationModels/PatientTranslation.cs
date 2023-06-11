using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class PatientTranslation
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Religion { get; set; }

    public string? Occupation { get; set; }

    public string? Employer { get; set; }

    public byte? RelationshipClient { get; set; }

    public int? PatientId { get; set; }

    public string? LangCode { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
    [JsonIgnore]
    public virtual Patient? Patient { get; set; }
}
