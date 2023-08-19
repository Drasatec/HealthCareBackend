using DomainModel.Entities.HospitalBody;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class HospitalTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? Description { get; set; }

    public int? HospitalId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual Hospital? Hospital { get; set; }
}
