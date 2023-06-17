using DomainModel.Helpers;

namespace DomainModel.Models.MedicalSpecialteis;

public class AllMedicalSpecialtyDto
{
    public int Total { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; } 
    public List<MedicalSpecialtyDto>? MedicalSpecialties { get; set; } = new List<MedicalSpecialtyDto>();
}
