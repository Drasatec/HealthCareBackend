using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class DoctorsDegree
{
    public short Id { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<DoctorsDegreesTranslation> DoctorsDegreesTranslations { get; set; } = new List<DoctorsDegreesTranslation>();
}
