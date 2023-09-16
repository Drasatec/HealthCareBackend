using DomainModel.Entities.HospitalBody;
using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.DoctorEntities;

public partial class DoctorWorkPeriod
{
    public int Id { get; set; }

    public int HospitalId { get; set; }

    public int SpecialtyId { get; set; }

    public int ClinicId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public byte DayId { get; set; }

    [JsonIgnore]
    public virtual Weekday? Day { get; set; } 

    //[JsonIgnore]
    public virtual Clinic? Clinic { get; set; } 

    [JsonIgnore]
    public virtual Doctor? Doctor { get; set; }

    [JsonIgnore]
    public virtual Hospital? Hospital { get; set; } 
    [JsonIgnore]
    public virtual MedicalSpecialty? Specialty { get; set; } 

    [JsonIgnore]
    public virtual WorkingPeriod? WorkingPeriod { get; set; }
}
