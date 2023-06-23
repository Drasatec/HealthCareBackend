using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class ClientGroup
{
    public int Id { get; set; }

    public string? GroupCode { get; set; }

    public string? GroupName { get; set; }

    public int? ClientId { get; set; }

    public virtual HosClient? Client { get; set; }

    public virtual ICollection<ClientsSubscription> ClientsSubscriptions { get; set; } = new List<ClientsSubscription>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
