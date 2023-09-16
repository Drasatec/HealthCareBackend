using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Religion
{
    public short Id { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<ReligionsTranslation> ReligionsTranslations { get; set; } = new List<ReligionsTranslation>();
}
