using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;
public partial class Country
{
    public short Id { get; set; }

    public string? CallingCode { get; set; }

    public byte? NumberOfDigits { get; set; }

    public string? CountryCode { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string CurrencySymbol { get; set; } = null!;

    public string? NationalFlag { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public virtual ICollection<CountriesTranslation> CountriesTranslations { get; set; } = new List<CountriesTranslation>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
