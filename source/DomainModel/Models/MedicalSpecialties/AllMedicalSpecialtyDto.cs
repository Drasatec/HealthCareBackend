using DomainModel.Helpers;

namespace DomainModel.Models.MedicalSpecialteis;

public class AllMedicalSpecialtyDto
{
    public int Total { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = Constants.PageSize;
    public List<MedicalSpecialtyDto>? MedicalSpecialties { get; set; } = new List<MedicalSpecialtyDto>();
}
