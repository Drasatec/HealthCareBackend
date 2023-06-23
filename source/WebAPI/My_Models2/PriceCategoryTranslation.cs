using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class PriceCategoryTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? PriceCategoryId { get; set; }

    public string? Description { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual PriceCategory? PriceCategory { get; set; }
}
