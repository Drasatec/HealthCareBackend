using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class DoctorTranslation
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Headline { get; set; }

    public string? About { get; set; }

    public int? DoctorId { get; set; }

    public string? LangCode { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
