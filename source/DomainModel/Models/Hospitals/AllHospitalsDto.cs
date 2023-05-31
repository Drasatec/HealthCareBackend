using System.Text.Json.Serialization;

namespace DomainModel.Models.Hospitals;

public  class AllHospitalsDto
{
   //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int Total { get; set; } = 0;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public List<HospitalDto>? Hospitals { get; set; } = new List<HospitalDto>();
}
