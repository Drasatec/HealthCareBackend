using DomainModel.Helpers;
using System.Text.Json.Serialization;

namespace DomainModel.Models.Hospitals;

public  class AllHospitalsDto
{
    public int Total { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public List<HospitalDto>? Hospitals { get; set; } = new List<HospitalDto>();
}
