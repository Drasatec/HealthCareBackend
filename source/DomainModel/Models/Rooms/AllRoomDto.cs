using DomainModel.Helpers;
using DomainModel.Models.Floors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models.Rooms;

public class AllRoomDto
{
    public int Total { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = Constants.PageSize;
    public List<RoomDto>? Rooms { get; set; } = new List<RoomDto>();
}
