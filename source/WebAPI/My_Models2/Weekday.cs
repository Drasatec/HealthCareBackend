using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Weekday
{
    public byte Id { get; set; }

    public virtual ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();

    public virtual ICollection<WeekdaysTranslation> WeekdaysTranslations { get; set; } = new List<WeekdaysTranslation>();
}
