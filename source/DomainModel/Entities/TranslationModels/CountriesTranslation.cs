using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class CountriesTranslation
{
    public short Id { get; set; }

    public string? CountryName { get; set; }

    public string? CurrencyName { get; set; }

    public string? CapitalName { get; set; }

    public short? CountryId { get; set; }

    public string? LangCode { get; set; }
    
    [JsonIgnore]
    public virtual Country? Country { get; set; }
}
