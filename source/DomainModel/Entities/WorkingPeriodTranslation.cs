using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class WorkingPeriodTranslation
{
    public string? Name { get; set; }

    public int WorkingPeriodId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual WorkingPeriod WorkingPeriod { get; set; } = null!;
}
