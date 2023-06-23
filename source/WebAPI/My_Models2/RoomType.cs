using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class RoomType
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public DateTime? CreateOn { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<HosRoom> HosRooms { get; set; } = new List<HosRoom>();

    public virtual ICollection<RoomTypeTranslation> RoomTypeTranslations { get; set; } = new List<RoomTypeTranslation>();
}
