using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class NationalitiesTranslation
{
    public string? Name { get; set; }

    public int NationalityId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual Nationality Nationality { get; set; } = null!;
}
