using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Doctor
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public byte? Gender { get; set; }

    public string? Photo { get; set; }

    public byte? WorkingHours { get; set; }

    public bool IsDeleted { get; set; }

    public byte? DocStatus { get; set; }

    public string? Reason { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsAppearanceOnSite { get; set; }

    public bool? PhoneNumberAppearance { get; set; }

    public bool? VisitPriceAppearance { get; set; }

    public short? DoctorsDegreeId { get; set; }

    public int? NationalityId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<DoctorTranslation> DoctorTranslations { get; set; } = new List<DoctorTranslation>();

    public virtual DoctorsDegree? DoctorsDegree { get; set; }

    public virtual ICollection<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; } = new List<DoctorsWorkHospital>();

    public virtual Nationality? Nationality { get; set; }

    public virtual ICollection<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; } = new List<PeriodWorkDoctorClinic>();

    public virtual ICollection<SpecialtiesDoctor> SpecialtiesDoctors { get; set; } = new List<SpecialtiesDoctor>();
}
