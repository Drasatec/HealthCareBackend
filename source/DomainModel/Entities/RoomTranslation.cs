using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class RoomTranslation
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int RoomId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual HosRoom Room { get; set; } = null!;
}
