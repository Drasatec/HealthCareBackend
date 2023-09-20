using DomainModel.Entities.HospitalBody;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.DoctorEntities;

public partial class DoctorsWorkHospital
{
    public int DoctorId { get; set; }

    public int HospitalId { get; set; }

    public DateTime? CreateOn { get; set; }
    
    [JsonIgnore]
    public virtual Doctor? Doctor { get; set; } 

    //[JsonIgnore]
    public virtual Hospital? Hospital { get; set; } 
}
