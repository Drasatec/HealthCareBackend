using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class TypesVisitTranslation
{
    public string? Name { get; set; }

    public int TypeVisitId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual TypesVisit TypeVisit { get; set; } = null!;
}
