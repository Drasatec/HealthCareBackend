using DomainModel.Entities.SettingsEntities;

namespace DomainModel.Entities.DoctorEntities;
public partial class DoctorVisitPrice
{
    public int Id { get; set; }

    public int? Price { get; set; }

    public string? PriceCurrency { get; set; }

    public int? DoctorId { get; set; }

    public int? PriceCategoryId { get; set; }

    public int? TypeVisitId { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual PriceCategory? PriceCategory { get; set; }

    public virtual TypesVisit? TypeVisit { get; set; }
}
