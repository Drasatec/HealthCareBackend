using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class MedicalSpecialtyTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string LangCode { get; set; } = null!;

    public int? MedicalSpecialtyId { get; set; }

    [JsonIgnore]
    public virtual MedicalSpecialty? MedicalSpecialty { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
