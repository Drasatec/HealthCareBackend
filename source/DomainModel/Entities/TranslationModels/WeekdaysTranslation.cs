using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class WeekdaysTranslation
{
    public short Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public short WeekdayId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual Weekday? Weekday { get; set; }
}
