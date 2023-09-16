using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class DoctorWorkPeriod
{
    public int Id { get; set; }

    public int HospitalId { get; set; }

    public int SpecialtyId { get; set; }

    public int ClinicId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public byte DayId { get; set; }

    public virtual Clinic Clinic { get; set; } = null!;

    public virtual Weekday Day { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual MedicalSpecialty Specialty { get; set; } = null!;

    public virtual WorkingPeriod WorkingPeriod { get; set; } = null!;
}
