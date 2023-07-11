using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class DoctorsDegree
{
    public short Id { get; set; }
    public DateTime? CreateOn { get; set; }

    [JsonIgnore]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<DoctorsDegreesTranslation> DoctorsDegreesTranslations { get; set; } = new List<DoctorsDegreesTranslation>();
}
