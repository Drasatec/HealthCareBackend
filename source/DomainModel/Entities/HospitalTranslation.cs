using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class HospitalTranslation
{
    public string? Name { get; set; }

    public string? Address { get; set; }

    public int HospitalId { get; set; }

    public string LangCode { get; set; } = null!;

    //public virtual Hospital Hospital { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
