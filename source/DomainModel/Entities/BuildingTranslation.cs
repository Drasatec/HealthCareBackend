using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class BuildingTranslation
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int BuildeingId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual HosBuilding Buildeing { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
