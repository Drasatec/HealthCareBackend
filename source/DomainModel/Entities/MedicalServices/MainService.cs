using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.MedicalServices;

public partial class MainService
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<MainServiceTranslation> MainServiceTranslations { get; set; } = new List<MainServiceTranslation>();

    public virtual ICollection<SecondaryService> SecondaryServices { get; set; } = new List<SecondaryService>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
