using System;
using System.Collections.Generic;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class Hospital
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? WhatsAppNumber { get; set; }

    public DateTime? CreateOn { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; } = new List<DoctorsWorkHospital>();

    public virtual ICollection<HosBuilding> HosBuildings { get; set; } = new List<HosBuilding>();

    public virtual ICollection<HosFloor> HosFloors { get; set; } = new List<HosFloor>();

    public virtual ICollection<HosRoom> HosRooms { get; set; } = new List<HosRoom>();

    public virtual ICollection<HospitalPhoneNumber> HospitalPhoneNumbers { get; set; } = new List<HospitalPhoneNumber>();

    public virtual ICollection<HospitalTranslation> HospitalTranslations { get; set; } = new List<HospitalTranslation>();

    public virtual ICollection<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; } = new List<PeriodWorkDoctorClinic>();

    public virtual ICollection<MedicalSpecialty> Specialties { get; set; } = new List<MedicalSpecialty>();
}
