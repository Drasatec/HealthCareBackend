using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Ssntype
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<SsntypesTranslation> SsntypesTranslations { get; set; } = new List<SsntypesTranslation>();
}
