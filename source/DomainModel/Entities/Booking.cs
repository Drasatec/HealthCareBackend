using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class Booking
{
    public int Id { get; set; }

    public int? HospitalId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? WorkingPeriodId { get; set; }

    public DateTime? VisitingDate { get; set; }

    public int? TypeVisitId { get; set; }

    public int? ClinicId { get; set; }


    public string? PriceCurrency { get; set; }

    public short? Price { get; set; }


    public DateTime? CreateOn { get; set; }

    public virtual Clinic? Clinic { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual TypesVisit? TypeVisit { get; set; }

    public virtual WorkingPeriod? WorkingPeriod { get; set; }
}
