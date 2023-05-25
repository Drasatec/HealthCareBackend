using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class Ssntype
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
