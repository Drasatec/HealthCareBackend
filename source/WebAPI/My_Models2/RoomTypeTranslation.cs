using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class RoomTypeTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? RoomTypeId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual RoomType? RoomType { get; set; }
}
