using DomainModel.Entities.HospitalBody;

namespace DomainModel.Entities.Others;

public class HospitalMedicalSpecialties
{
    public int HospitalId { get; set; }
    public virtual Hospital? Hospital { get; set; }

    public int SpecialtyId { get; set; }
    public virtual MedicalSpecialty? Specialty { get; set; }
}
