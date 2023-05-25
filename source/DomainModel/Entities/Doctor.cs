using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class Doctor
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public byte? Gender { get; set; }

    public string? Photo { get; set; }

    public byte? WorkingHours { get; set; }

    public byte? DocStatus { get; set; }

    public string? Reason { get; set; }

    public bool? IsAppearanceOnSite { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberStatus { get; set; }

    public bool IsDeleted { get; set; }

    public short? DoctorsDegreeId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<DoctorTranslation> DoctorTranslations { get; set; } = new List<DoctorTranslation>();

    public virtual DoctorsDegree? DoctorsDegree { get; set; }

    public virtual ICollection<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; } = new List<DoctorsWorkHospital>();

    public virtual ICollection<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; } = new List<PeriodWorkDoctorClinic>();

    public virtual ICollection<MedicalSpecialty> MedicalSpecialties { get; set; } = new List<MedicalSpecialty>();
}
