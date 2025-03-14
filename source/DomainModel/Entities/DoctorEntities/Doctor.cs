﻿using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.TranslationModels;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.DoctorEntities;

public partial class Doctor
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public byte? Gender { get; set; }

    public string? Photo { get; set; }

    public byte? WorkingHours { get; set; }

    public byte? DocStatus { get; set; }

    public string? Reason { get; set; }

    public bool IsAppearanceOnSite { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberAppearance { get; set; }

    public bool VisitPriceAppearance { get; set; }

    public bool IsDeleted { get; set; }

    public short? DoctorsDegreeId { get; set; }

    public int? NationalityId { get; set; }


    public virtual Nationality? Nationality { get; set; }

    public virtual DoctorsDegree? DoctorsDegree { get; set; }

    public virtual ICollection<DoctorTranslation> DoctorTranslations { get; set; } = new List<DoctorTranslation>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; } = new List<DoctorsWorkHospital>();

    //[JsonIgnore]
    public virtual ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();


    public virtual ICollection<SpecialtiesDoctor> SpecialtiesDoctors { get; set; } = new List<SpecialtiesDoctor>();
    public virtual ICollection<DoctorAttachment> DoctorAttachments { get; set; } = new List<DoctorAttachment>();

}
