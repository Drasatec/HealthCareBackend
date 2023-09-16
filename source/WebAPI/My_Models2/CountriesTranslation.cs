using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class CountriesTranslation
{
    public short Id { get; set; }

    public string? CountryName { get; set; }

    public string? CurrencyName { get; set; }

    public string? CapitalName { get; set; }

    public short? CountryId { get; set; }

    public string? LangCode { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
