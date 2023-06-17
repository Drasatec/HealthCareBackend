using DomainModel.Helpers;

namespace DomainModel.Models.Floors;

public class AllFloorDto
{
    public int Total { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public List<FloorDto>? Floors { get; set; } = new List<FloorDto>();
}
