using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Gender
{
    public short Id { get; set; }

    public virtual ICollection<GendersTranslation> GendersTranslations { get; set; } = new List<GendersTranslation>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
