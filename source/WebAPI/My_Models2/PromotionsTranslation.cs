using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class PromotionsTranslation
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? PromotionId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual Promotion? Promotion { get; set; }
}
