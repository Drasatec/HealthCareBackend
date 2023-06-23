using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class NationalitiesTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NationalityId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual Nationality? Nationality { get; set; }
}
