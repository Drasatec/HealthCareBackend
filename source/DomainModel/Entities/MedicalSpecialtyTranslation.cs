using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class MedicalSpecialtyTranslation
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int MedicalSpecialtyId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual MedicalSpecialty MedicalSpecialty { get; set; } = null!;
}
