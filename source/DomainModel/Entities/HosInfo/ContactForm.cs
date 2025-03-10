﻿using DomainModel.Entities.HospitalBody;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.HosInfo;

public partial class ContactForm
{
    public int Id { get; set; }

    public string SenderName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? ContactDate { get; set; }

    public int? HospitalId { get; set; }

    [JsonIgnore]
    public virtual Hospital? Hospital { get; set; }
}
