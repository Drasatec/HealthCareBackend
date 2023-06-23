using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class MedicalSpecialtyTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? MedicalSpecialtyId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual MedicalSpecialty? MedicalSpecialty { get; set; }
}
