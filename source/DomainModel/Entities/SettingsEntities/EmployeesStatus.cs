using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class EmployeesStatus
{
    public short Id { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual ICollection<EmployeesStatusTranslation> EmployeesStatusTranslations { get; set; } = new List<EmployeesStatusTranslation>();
}
