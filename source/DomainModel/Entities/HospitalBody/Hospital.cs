﻿using DomainModel.Entities.DoctorEntities;
using DomainModel.Entities.HosInfo;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.HospitalBody;

public partial class Hospital
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? WhatsAppNumber { get; set; }

    public DateTime? CreateOn { get; set; }

    public bool IsDeleted { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public virtual ICollection<ContactForm> ContactForms { get; set; } = new List<ContactForm>();

    public virtual ICollection<HospitalFeature> HospitalFeatures { get; set; } = new List<HospitalFeature>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; } = new List<DoctorsWorkHospital>();

    public virtual ICollection<HosBuilding> HosBuildings { get; set; } = new List<HosBuilding>();

    public virtual ICollection<HosFloor> HosFloors { get; set; } = new List<HosFloor>();

    public virtual ICollection<HosRoom> HosRooms { get; set; } = new List<HosRoom>();

    public virtual ICollection<HospitalPhoneNumber> HospitalPhoneNumbers { get; set; } = new List<HospitalPhoneNumber>();

    public virtual ICollection<HospitalTranslation> HospitalTranslations { get; set; } = new List<HospitalTranslation>();

    //public virtual ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();

    public virtual ICollection<MedicalSpecialty> Specialties { get; set; } = new List<MedicalSpecialty>();


}
