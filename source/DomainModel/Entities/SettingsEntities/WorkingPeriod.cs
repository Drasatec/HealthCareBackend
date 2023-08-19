using DomainModel.Entities.TranslationModels;
namespace DomainModel.Entities.SettingsEntities;

public partial class WorkingPeriod
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public virtual ICollection<WorkingPeriodTranslation> WorkingPeriodTranslations { get; set; } = new List<WorkingPeriodTranslation>();

}
