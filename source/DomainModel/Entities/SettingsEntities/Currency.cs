namespace DomainModel.Entities.SettingsEntities;

public partial class Currency
{
    public int Id { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string CurrencyName { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public string Country { get; set; } = null!;

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

}
