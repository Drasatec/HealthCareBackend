using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.HospitalBody;

namespace DomainModel.Entities.TranslationModels;

public partial class HospitalTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? Description { get; set; }

    public int? HospitalId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual Hospital? Hospital { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
