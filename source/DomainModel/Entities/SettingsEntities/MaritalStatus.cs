using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class MaritalStatus
{
    public short Id { get; set; }

    public virtual ICollection<MaritalStatusTranslation> MaritalStatusTranslations { get; set; } = new List<MaritalStatusTranslation>();
}
