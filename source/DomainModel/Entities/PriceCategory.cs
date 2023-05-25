using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class PriceCategory
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual ICollection<ClientsSubscription> ClientsSubscriptions { get; set; } = new List<ClientsSubscription>();

    public virtual ICollection<PriceCategoryTranslation> PriceCategoryTranslations { get; set; } = new List<PriceCategoryTranslation>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
