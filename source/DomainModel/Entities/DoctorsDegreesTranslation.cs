using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class DoctorsDegreesTranslation
{
    public string? DegreeName { get; set; }

    public short DoctorDegreeId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual DoctorsDegree DoctorDegree { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
