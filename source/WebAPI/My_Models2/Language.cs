﻿using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Language
{
    public string Code { get; set; } = null!;

    public string? LanguageName { get; set; }

    public virtual ICollection<BuildingTranslation> BuildingTranslations { get; set; } = new List<BuildingTranslation>();

    public virtual ICollection<ClinicTranslation> ClinicTranslations { get; set; } = new List<ClinicTranslation>();

    public virtual ICollection<DoctorTranslation> DoctorTranslations { get; set; } = new List<DoctorTranslation>();

    public virtual ICollection<DoctorsDegreesTranslation> DoctorsDegreesTranslations { get; set; } = new List<DoctorsDegreesTranslation>();

    public virtual ICollection<EmployeesStatusTranslation> EmployeesStatusTranslations { get; set; } = new List<EmployeesStatusTranslation>();

    public virtual ICollection<FloorTranslation> FloorTranslations { get; set; } = new List<FloorTranslation>();

    public virtual ICollection<Gender> Genders { get; set; } = new List<Gender>();

    public virtual ICollection<HospitalTranslation> HospitalTranslations { get; set; } = new List<HospitalTranslation>();

    public virtual ICollection<MainServiceTranslation> MainServiceTranslations { get; set; } = new List<MainServiceTranslation>();

    public virtual ICollection<MedicalSpecialtyTranslation> MedicalSpecialtyTranslations { get; set; } = new List<MedicalSpecialtyTranslation>();

    public virtual ICollection<NationalitiesTranslation> NationalitiesTranslations { get; set; } = new List<NationalitiesTranslation>();

    public virtual ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();

    public virtual ICollection<PriceCategoryTranslation> PriceCategoryTranslations { get; set; } = new List<PriceCategoryTranslation>();

    public virtual ICollection<RoomTranslation> RoomTranslations { get; set; } = new List<RoomTranslation>();

    public virtual ICollection<RoomTypeTranslation> RoomTypeTranslations { get; set; } = new List<RoomTypeTranslation>();

    public virtual ICollection<SecondaryServiceTranslation> SecondaryServiceTranslations { get; set; } = new List<SecondaryServiceTranslation>();

    public virtual ICollection<ServiceTranslation> ServiceTranslations { get; set; } = new List<ServiceTranslation>();

    public virtual ICollection<SsntypesTranslation> SsntypesTranslations { get; set; } = new List<SsntypesTranslation>();

    public virtual ICollection<TypesVisitTranslation> TypesVisitTranslations { get; set; } = new List<TypesVisitTranslation>();

    public virtual ICollection<Weekday> Weekdays { get; set; } = new List<Weekday>();

    public virtual ICollection<WorkingPeriodTranslation> WorkingPeriodTranslations { get; set; } = new List<WorkingPeriodTranslation>();
}