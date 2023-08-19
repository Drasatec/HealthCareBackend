using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class WorkingPeriodTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? WorkingPeriodId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual WorkingPeriod? WorkingPeriod { get; set; }
}
