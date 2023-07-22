using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class Booking
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int HospitalId { get; set; }

    public int SpecialtyId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public int TypeVisitId { get; set; }

    public int? ClinicId { get; set; }

    public int? PriceCategoryId { get; set; }

    public int? CurrencyId { get; set; }

    public short? BookingStatusId { get; set; }

    public int? Price { get; set; }

    public DateTime? VisitingDate { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual BookingStatus? BookingStatus { get; set; }

    public virtual Clinic? Clinic { get; set; }

    public virtual Currency? Currency { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual PriceCategory? PriceCategory { get; set; }

    public virtual MedicalSpecialty Specialty { get; set; } = null!;

    public virtual TypesVisit TypeVisit { get; set; } = null!;

    public virtual WorkingPeriod WorkingPeriod { get; set; } = null!;
}
