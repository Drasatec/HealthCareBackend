using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class BuildingTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? BuildeingId { get; set; }

    public string? LangCode { get; set; }

    public virtual HosBuilding? Buildeing { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
