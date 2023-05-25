using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class RoomTypeTranslation
{
    public string? Name { get; set; }

    public int RoomTypeId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual RoomType RoomType { get; set; } = null!;
}
