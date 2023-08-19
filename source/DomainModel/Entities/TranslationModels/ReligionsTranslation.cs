using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class ReligionsTranslation
{
    public short Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public short? ReligionId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual Religion? Religion { get; set; }
}
