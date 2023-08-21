using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Weekday
{
    public short Id { get; set; }

    public virtual ICollection<WeekdaysTranslation> WeekdaysTranslations { get; set; } = new List<WeekdaysTranslation>();
}
