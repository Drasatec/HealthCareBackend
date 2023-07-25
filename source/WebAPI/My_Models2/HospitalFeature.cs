using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class HospitalFeature
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? HospitalId { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual ICollection<HospitalFeatureTranslation> HospitalFeatureTranslations { get; set; } = new List<HospitalFeatureTranslation>();
}
