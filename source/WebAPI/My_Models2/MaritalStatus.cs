using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class MaritalStatus
{
    public short Id { get; set; }

    public virtual ICollection<MaritalStatusTranslation> MaritalStatusTranslations { get; set; } = new List<MaritalStatusTranslation>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
