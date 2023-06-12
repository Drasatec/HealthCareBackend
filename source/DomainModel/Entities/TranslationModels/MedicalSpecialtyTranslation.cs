using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class MedicalSpecialtyTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? MedicalSpecialtyId { get; set; }

    public string LangCode { get; set; } = null!;
    [JsonIgnore]
    public virtual MedicalSpecialty? MedicalSpecialty { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
