using DomainModel.Models.Hospitals;

namespace DomainModel.Models.Buildings;

public class AllBuildingDto
{
    public int Total { get; set; } = 0;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public List<BuildingDto>? Buildings { get; set; } = new List<BuildingDto>();
}
