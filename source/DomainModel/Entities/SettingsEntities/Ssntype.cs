using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class Ssntype
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public virtual ICollection<SsntypesTranslation> SsntypesTranslations { get; set; } = new List<SsntypesTranslation>();

}
