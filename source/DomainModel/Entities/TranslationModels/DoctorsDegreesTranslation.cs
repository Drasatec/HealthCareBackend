using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.DoctorEntities;

namespace DomainModel.Entities.TranslationModels;

public partial class DoctorsDegreesTranslation
{
    public int Id { get; set; }

    public string DegreeName { get; set; } = string.Empty;

    public short? DoctorDegreeId { get; set; }

    public string LangCode { get; set; } = null!;
    [JsonIgnore]
    public virtual DoctorsDegree? DoctorDegree { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
