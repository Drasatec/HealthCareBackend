using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class DoctorTranslation
{
    public string? FullName { get; set; }

    public string? Headline { get; set; }

    public string? About { get; set; }

    public int DoctorId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
