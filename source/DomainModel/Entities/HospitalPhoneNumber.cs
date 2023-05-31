using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class HospitalPhoneNumber
{
    public int Id { get; set; }

    public string? TelephoneNumber { get; set; }

    public int HospitalId { get; set; }

    //public virtual Hospital? Hospital { get; set; }
}
