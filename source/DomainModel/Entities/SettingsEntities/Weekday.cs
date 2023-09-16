using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;
public class Weekday
{
    public byte Id { get; set; }

    public virtual ICollection<WeekdaysTranslation> WeekdaysTranslations { get; set; } = new List<WeekdaysTranslation>();
}