using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class MaritalStatus
{
    public byte Id { get; set; }

    public virtual ICollection<MaritalStatusTranslation> MaritalStatusTranslations { get; set; } = new List<MaritalStatusTranslation>();
}
