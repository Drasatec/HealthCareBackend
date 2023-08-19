using DomainModel.Entities.SettingsEntities;

namespace DomainModel.Entities.MedicalServices;

public partial class ServicePrice
{
    public int ServiceId { get; set; }

    public int PriceCategoryId { get; set; }

    public short? Price { get; set; }

    public string? PriceCurrency { get; set; }

    public string? Note { get; set; }

    public virtual PriceCategory PriceCategory { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
