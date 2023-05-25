using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class HospitalsContactDatum
{
    public int Id { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? Email { get; set; }

    public string? WhatsAppNumber { get; set; }

    public int? HospitalId { get; set; }

    //public virtual Hospital? Hospital { get; set; }
}
