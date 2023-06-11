using System;
using System.Collections.Generic;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class HosRoom
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public string CodeNumber { get; set; } =string.Empty;

    public string? Kind { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int HospitalId { get; set; }

    public int BuildId { get; set; }

    public int FloorId { get; set; }

    public int RoomTypeId { get; set; }

    public virtual HosBuilding Build { get; set; } = null!;

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual HosFloor Floor { get; set; } = null!;

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual ICollection<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; } = new List<PeriodWorkDoctorClinic>();

    public virtual ICollection<RoomTranslation> RoomTranslations { get; set; } = new List<RoomTranslation>();

    public virtual RoomType RoomType { get; set; } = null!;
}
