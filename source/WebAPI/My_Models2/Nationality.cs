using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Nationality
{
    public int Id { get; set; }

    public string? Symbol { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<NationalitiesTranslation> NationalitiesTranslations { get; set; } = new List<NationalitiesTranslation>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
