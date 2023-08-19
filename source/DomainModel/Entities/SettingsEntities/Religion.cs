using DomainModel.Entities.TranslationModels;
namespace DomainModel.Entities.SettingsEntities;

public partial class Religion
{
    public short Id { get; set; }

    public virtual ICollection<ReligionsTranslation> ReligionsTranslations { get; set; } = new List<ReligionsTranslation>();
}
