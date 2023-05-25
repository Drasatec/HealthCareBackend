using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class SecondaryServiceTranslation
{
    public string? Name { get; set; }

    public int SecondaryServiceId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual SecondaryService SecondaryService { get; set; } = null!;
}
