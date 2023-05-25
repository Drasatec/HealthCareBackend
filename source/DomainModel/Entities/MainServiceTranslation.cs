using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class MainServiceTranslation
{
    public string? Name { get; set; }

    public int MainServiceId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual MainService MainService { get; set; } = null!;
}
