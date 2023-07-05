﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class Clinic
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public string? Photo { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? Reason { get; set; }

    public string? Phone { get; set; }

    public short? WorkingHours { get; set; }

    public bool Appearance { get; set; }

    public int HospitalId { get; set; }

    public int BuildId { get; set; }

    public int FloorId { get; set; }

    public int RoomId { get; set; }

    public int SpecialtyId { get; set; }

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual HosBuilding Build { get; set; } = null!;

    public virtual HosRoom Room { get; set; } = null!;

    public virtual HosFloor Floor { get; set; } = null!;

    public virtual ICollection<ClinicTranslation> ClinicTranslations { get; set; } = new List<ClinicTranslation>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [JsonIgnore]
    public virtual ICollection<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; } = new List<PeriodWorkDoctorClinic>();

    public virtual MedicalSpecialty Specialty { get; set; } = null!;
}
