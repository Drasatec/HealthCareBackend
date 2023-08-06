using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.SettingsEntities;

namespace DomainModel.Entities.DoctorEntities;

public partial class DoctorWorkPeriod
{
    public int Id { get; set; }

    public int HospitalId { get; set; }

    public int SpecialtyId { get; set; }

    public int ClinicId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public byte OnDay { get; set; }

    //public virtual Hospital? Hospital { get; set; }

    //public virtual MedicalSpecialty? MedicalSpecialty { get; set; }

    // public virtual Clinic? Clinic { get; set; } = null!;

    [JsonIgnore]
    public virtual Doctor? Doctor { get; set; } = null!;

    [JsonIgnore]
    public virtual WorkingPeriod? WorkingPeriod { get; set; } = null!;
}
