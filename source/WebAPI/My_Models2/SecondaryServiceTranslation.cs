using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class SecondaryServiceTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? SecondaryServiceId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual SecondaryService? SecondaryService { get; set; }
}
