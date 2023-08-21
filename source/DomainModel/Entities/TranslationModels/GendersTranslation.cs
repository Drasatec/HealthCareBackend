using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class GendersTranslation
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public short GenderId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual Gender? Gender { get; set; }
}
