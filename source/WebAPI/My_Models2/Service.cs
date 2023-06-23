using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Service
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public string? Code { get; set; }

    public string? Photo { get; set; }

    public bool? IsActive { get; set; }

    public string? Reason { get; set; }

    public int? SecondaryServiceId { get; set; }

    public int? MainServiceId { get; set; }

    public virtual MainService? MainService { get; set; }

    public virtual SecondaryService? SecondaryService { get; set; }

    public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();

    public virtual ICollection<ServiceTranslation> ServiceTranslations { get; set; } = new List<ServiceTranslation>();
}
