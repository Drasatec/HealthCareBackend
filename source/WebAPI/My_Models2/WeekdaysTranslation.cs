using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class WeekdaysTranslation
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public byte? WeekdayId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual Weekday? Weekday { get; set; }
}
