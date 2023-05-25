using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class Nationality
{
    public int Id { get; set; }

    public virtual ICollection<NationalitiesTranslation> NationalitiesTranslations { get; set; } = new List<NationalitiesTranslation>();

    public virtual ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();
}
