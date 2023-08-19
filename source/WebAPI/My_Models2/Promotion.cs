using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Promotion
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public int? Position { get; set; }

    public string? Link { get; set; }

    public virtual ICollection<PromotionsTranslation> PromotionsTranslations { get; set; } = new List<PromotionsTranslation>();
}
