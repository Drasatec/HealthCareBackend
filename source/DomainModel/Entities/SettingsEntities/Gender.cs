using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class Gender
{
    public short Id { get; set; }

    public virtual ICollection<GendersTranslation> GendersTranslations { get; set; } = new List<GendersTranslation>();

}
