using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class MedicalSpecialty
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? Reason { get; set; }

    public bool? Appearance { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();

    public virtual ICollection<MedicalSpecialtyTranslation> MedicalSpecialtyTranslations { get; set; } = new List<MedicalSpecialtyTranslation>();

    public virtual ICollection<SpecialtiesDoctor> SpecialtiesDoctors { get; set; } = new List<SpecialtiesDoctor>();

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}
