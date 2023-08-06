using System;
using System.Collections.Generic;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class Nationality
{
    public int Id { get; set; }

    public string? Symbol { get; set; }

    public virtual ICollection<NationalitiesTranslation> NationalitiesTranslations { get; set; } = new List<NationalitiesTranslation>();

    //public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    // public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
