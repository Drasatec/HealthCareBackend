using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class ServiceTranslation
{
    public string? Name { get; set; }

    public int ServiceId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
