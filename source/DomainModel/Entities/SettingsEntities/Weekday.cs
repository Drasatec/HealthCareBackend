using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;
public class Weekday
{
    public short Id { get; set; }

    public virtual ICollection<WeekdaysTranslation> WeekdaysTranslations { get; set; } = new List<WeekdaysTranslation>();
}