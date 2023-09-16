using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class PatientTranslation
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Occupation { get; set; }

    public string? Address { get; set; }

    public int? PatientId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual Patient? Patient { get; set; }
}
