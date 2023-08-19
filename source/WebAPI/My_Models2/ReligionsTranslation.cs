using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class ReligionsTranslation
{
    public byte Id { get; set; }

    public string? Name { get; set; }

    public byte? ReligionId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual Religion? Religion { get; set; }
}
