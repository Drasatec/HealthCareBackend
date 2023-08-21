using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class GendersTranslation
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public short? GenderId { get; set; }

    public string? LangCode { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
