using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class PriceCategory
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public DateTime? CreateOn { get; set; }

    public string? Symbol { get; set; }

    public virtual ICollection<ClientsSubscription> ClientsSubscriptions { get; set; } = new List<ClientsSubscription>();

    public virtual ICollection<DoctorVisitPrice> DoctorVisitPrices { get; set; } = new List<DoctorVisitPrice>();

    public virtual ICollection<PriceCategoryTranslation> PriceCategoryTranslations { get; set; } = new List<PriceCategoryTranslation>();

    public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();
}
