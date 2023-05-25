using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class FloorTranslation
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int FloorId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual HosFloor Floor { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
