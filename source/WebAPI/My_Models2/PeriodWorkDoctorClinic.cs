using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class PeriodWorkDoctorClinic
{
    public int Id { get; set; }

    public int? HospitalId { get; set; }

    public int? ClinicId { get; set; }

    public int? DoctorId { get; set; }

    public int? WorkingPeriodId { get; set; }

    public byte? OnDay { get; set; }

    public virtual Clinic? Clinic { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual WorkingPeriod? WorkingPeriod { get; set; }
}
