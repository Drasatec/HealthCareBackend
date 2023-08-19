using DomainModel.Entities.HospitalBody;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class BuildingTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? BuildeingId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual HosBuilding? Buildeing { get; set; }
}
