using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class ClinicTranslation
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string LangCode { get; set; } = null!;

    public int ClinicId { get; set; }

    public virtual Clinic Clinic { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
