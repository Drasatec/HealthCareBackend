using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class HospitalTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public int? HospitalId { get; set; }

    public string? LangCode { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
