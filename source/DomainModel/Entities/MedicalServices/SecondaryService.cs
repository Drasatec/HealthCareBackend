using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.MedicalServices;

public partial class SecondaryService
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public int? MainServiceId { get; set; }

    public virtual MainService? MainService { get; set; }

    public virtual ICollection<SecondaryServiceTranslation> SecondaryServiceTranslations { get; set; } = new List<SecondaryServiceTranslation>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
