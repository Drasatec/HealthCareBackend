using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class MaritalStatusTranslation
{
    public short Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public short? MaritalId { get; set; }

    public string? LangCode { get; set; }

    [JsonIgnore]
    public virtual MaritalStatus? Marital { get; set; }
}
