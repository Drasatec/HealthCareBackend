﻿namespace DomainModel.Entities.Others;
public partial class ClientGroup
{
    public int Id { get; set; }

    public string? GroupCode { get; set; }

    public string? GroupName { get; set; }

    public int? ClientId { get; set; }

    public virtual HosClient? Client { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
