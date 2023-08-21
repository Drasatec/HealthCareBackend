using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class MaritalStatusTranslation
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public short? MaritalId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual MaritalStatus? Marital { get; set; }
}
