using System;
using System.Collections.Generic;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class HosBuilding
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public string? Photo { get; set; }

    public DateTime? CreateOn { get; set; }

    public bool IsDeleted { get; set; }

    public int HospitalId { get; set; }

    public virtual ICollection<BuildingTranslation> BuildingTranslations { get; set; } = new List<BuildingTranslation>();

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<HosFloor> HosFloors { get; set; } = new List<HosFloor>();

    public virtual ICollection<HosRoom> HosRooms { get; set; } = new List<HosRoom>();

    public virtual Hospital Hospital { get; set; } = null!;

    //public virtual ICollection<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; } = new List<PeriodWorkDoctorClinic>();
}
