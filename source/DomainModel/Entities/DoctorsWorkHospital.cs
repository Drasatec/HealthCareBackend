using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class DoctorsWorkHospital
{
    public int DoctorId { get; set; }

    public int HospitalId { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Hospital Hospital { get; set; } = null!;
}
