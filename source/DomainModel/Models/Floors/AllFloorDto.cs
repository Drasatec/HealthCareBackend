using DomainModel.Helpers;

namespace DomainModel.Models.Floors;

public class AllFloorDto
{
    public int Total { get; set; } 
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = Constants.PageSize;
    public List<FloorDto>? Floors { get; set; } = new List<FloorDto>();
}
