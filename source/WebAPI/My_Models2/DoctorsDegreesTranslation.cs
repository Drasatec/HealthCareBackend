using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class DoctorsDegreesTranslation
{
    public int Id { get; set; }

    public string? DegreeName { get; set; }

    public short? DoctorDegreeId { get; set; }

    public string? LangCode { get; set; }

    public virtual DoctorsDegree? DoctorDegree { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
