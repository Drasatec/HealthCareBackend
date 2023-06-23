using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;
public partial class MedicalSpecialty
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? Reason { get; set; }

    public bool Appearance { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<MedicalSpecialtyTranslation> MedicalSpecialtyTranslations { get; set; } = new List<MedicalSpecialtyTranslation>();

    public virtual ICollection<SpecialtiesDoctor> SpecialtiesDoctors { get; set; } = new List<SpecialtiesDoctor>();

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();

}
