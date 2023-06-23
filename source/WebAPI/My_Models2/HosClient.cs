using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class HosClient
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public string? NameEn { get; set; }

    public string? NameOriginalLang { get; set; }

    public string? Address { get; set; }

    public string? Photo { get; set; }

    public string? TelephoneNumber1 { get; set; }

    public string? TelephoneNumber2 { get; set; }

    public string? Email { get; set; }

    public string? WhatsAppNumber { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public byte? ClientStatus { get; set; }

    public string? Reason { get; set; }

    public virtual ICollection<ClientGroup> ClientGroups { get; set; } = new List<ClientGroup>();

    public virtual ICollection<ClientsSubscription> ClientsSubscriptions { get; set; } = new List<ClientsSubscription>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
