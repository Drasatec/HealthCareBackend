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

    public virtual ICollection<DoctorAttachment> DoctorAttachments { get; set; } = new List<DoctorAttachment>();

    public virtual ICollection<DoctorTranslation> DoctorTranslations { get; set; } = new List<DoctorTranslation>();

    public virtual ICollection<DoctorVisitPrice> DoctorVisitPrices { get; set; } = new List<DoctorVisitPrice>();

    public virtual ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();

    public virtual DoctorsDegree? DoctorsDegree { get; set; }

    public virtual ICollection<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; } = new List<DoctorsWorkHospital>();

    public virtual Nationality? Nationality { get; set; }

    public virtual ICollection<SpecialtiesDoctor> SpecialtiesDoctors { get; set; } = new List<SpecialtiesDoctor>();
}
