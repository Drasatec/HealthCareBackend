﻿using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class ClientGroup
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public string? GroupName { get; set; }

    public int? ClientId { get; set; }

    public virtual HosClient? Client { get; set; }

    public virtual ICollection<ClientsSubscription> ClientsSubscriptions { get; set; } = new List<ClientsSubscription>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}