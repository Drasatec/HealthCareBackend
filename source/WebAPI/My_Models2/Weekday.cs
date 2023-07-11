using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Weekday
{
    public int Id { get; set; }

    public byte DayNumber { get; set; }

    public string WeekdayName { get; set; } = null!;

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
