using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Language
{
    public string Code { get; set; } = null!;

    public string? LanguageName { get; set; }

    public bool IsDefault { get; set; }

    public virtual ICollection<BookingStatusesTranslation> BookingStatusesTranslations { get; set; } = new List<BookingStatusesTranslation>();

    public virtual ICollection<BuildingTranslation> BuildingTranslations { get; set; } = new List<BuildingTranslation>();

    public virtual ICollection<ClinicTranslation> ClinicTranslations { get; set; } = new List<ClinicTranslation>();

    public virtual ICollection<CountriesTranslation> CountriesTranslations { get; set; } = new List<CountriesTranslation>();

    public virtual ICollection<DoctorTranslation> DoctorTranslations { get; set; } = new List<DoctorTranslation>();

    public virtual ICollection<DoctorsDegreesTranslation> DoctorsDegreesTranslations { get; set; } = new List<DoctorsDegreesTranslation>();

    public virtual ICollection<EmployeesStatusTranslation> EmployeesStatusTranslations { get; set; } = new List<EmployeesStatusTranslation>();

    public virtual ICollection<FloorTranslation> FloorTranslations { get; set; } = new List<FloorTranslation>();

    public virtual ICollection<GendersTranslation> GendersTranslations { get; set; } = new List<GendersTranslation>();

    public virtual ICollection<HospitalFeatureTranslation> HospitalFeatureTranslations { get; set; } = new List<HospitalFeatureTranslation>();

    public virtual ICollection<HospitalTranslation> HospitalTranslations { get; set; } = new List<HospitalTranslation>();

    public virtual ICollection<MaritalStatusTranslation> MaritalStatusTranslations { get; set; } = new List<MaritalStatusTranslation>();

    public virtual ICollection<MedicalSpecialtyTranslation> MedicalSpecialtyTranslations { get; set; } = new List<MedicalSpecialtyTranslation>();

    public virtual ICollection<NationalitiesTranslation> NationalitiesTranslations { get; set; } = new List<NationalitiesTranslation>();

    public virtual ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();

    public virtual ICollection<PriceCategoryTranslation> PriceCategoryTranslations { get; set; } = new List<PriceCategoryTranslation>();

    public virtual ICollection<PromotionsTranslation> PromotionsTranslations { get; set; } = new List<PromotionsTranslation>();

    public virtual ICollection<ReligionsTranslation> ReligionsTranslations { get; set; } = new List<ReligionsTranslation>();

    public virtual ICollection<RoomTranslation> RoomTranslations { get; set; } = new List<RoomTranslation>();

    public virtual ICollection<RoomTypeTranslation> RoomTypeTranslations { get; set; } = new List<RoomTypeTranslation>();

    public virtual ICollection<SsntypesTranslation> SsntypesTranslations { get; set; } = new List<SsntypesTranslation>();

    public virtual ICollection<TypesVisitTranslation> TypesVisitTranslations { get; set; } = new List<TypesVisitTranslation>();

    public virtual ICollection<WeekdaysTranslation> WeekdaysTranslations { get; set; } = new List<WeekdaysTranslation>();

    public virtual ICollection<WorkingPeriodTranslation> WorkingPeriodTranslations { get; set; } = new List<WorkingPeriodTranslation>();
}
