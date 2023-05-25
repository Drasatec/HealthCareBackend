using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class PeriodWorkDoctorClinic
{
    public int? HospitalId { get; set; }

    public int? BuildId { get; set; }

    public int? FloorId { get; set; }

    public int? RoomId { get; set; }

    public int ClinicId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public byte? OnDay { get; set; }

    public virtual HosBuilding? Build { get; set; }

    public virtual Clinic Clinic { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual HosFloor? Floor { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual HosRoom? Room { get; set; }

    public virtual WorkingPeriod WorkingPeriod { get; set; } = null!;
}
