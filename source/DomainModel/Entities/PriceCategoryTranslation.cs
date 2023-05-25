using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class PriceCategoryTranslation
{
    public string? Name { get; set; }

    public int PriceCategoryId { get; set; }

    public string? Description { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual PriceCategory PriceCategory { get; set; } = null!;
}
