using System;
using System.Collections.Generic;
using DomainModel.Entities.SettingsEntities;

namespace DomainModel.Entities;

public partial class ClientsSubscription
{
    public string? Specification { get; set; }

    public short? NumberOfSubscribers { get; set; }

    public DateTime? Duration { get; set; }

    public TimeSpan? StartDate { get; set; }

    public short? PriceOfSubscription { get; set; }

    public string? PriceCurrency { get; set; }

    public byte Status { get; set; }

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? PriceCategoryId { get; set; }

    public int ClientId { get; set; }

    public int ClientGroupId { get; set; }

    public virtual HosClient Client { get; set; } = null!;

    public virtual ClientGroup ClientGroup { get; set; } = null!;

    public virtual PriceCategory? PriceCategory { get; set; }
}
