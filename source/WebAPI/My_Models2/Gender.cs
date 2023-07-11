using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Gender
{
    public int Id { get; set; }

    public byte GenderNumber { get; set; }

    public string GenderName { get; set; } = null!;

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;
}
