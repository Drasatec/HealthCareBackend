using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Booking
{
    public long Id { get; set; }

    public string BookingNumber { get; set; } = null!;

    public int PatientId { get; set; }

    public int HospitalId { get; set; }

    public int SpecialtyId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public int TypeVisitId { get; set; }

    public int ClinicId { get; set; }

    public short BookingStatusId { get; set; }

    public int? PriceCategoryId { get; set; }

    public int? CurrencyId { get; set; }

    public int? Price { get; set; }

    public byte? DayNumber { get; set; }

    public DateTime? VisitingDate { get; set; }

    public string? BookingReason { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual BookingStatus BookingStatus { get; set; } = null!;

    public virtual Clinic Clinic { get; set; } = null!;

    public virtual Currency? Currency { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual PriceCategory? PriceCategory { get; set; }

    public virtual MedicalSpecialty Specialty { get; set; } = null!;

    public virtual TypesVisit TypeVisit { get; set; } = null!;

    public virtual WorkingPeriod WorkingPeriod { get; set; } = null!;
}
