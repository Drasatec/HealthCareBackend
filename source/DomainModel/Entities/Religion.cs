using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class Religion
{
    public byte Id { get; set; }

    public virtual ICollection<ReligionsTranslation> ReligionsTranslations { get; set; } = new List<ReligionsTranslation>();
}
