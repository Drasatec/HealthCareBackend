using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class PatientTranslation
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string? Occupation { get; set; }

    [JsonIgnore]
    public string? Employer { get; set; }

    [JsonIgnore]
    public byte? RelationshipClient { get; set; }

    public int? PatientId { get; set; }

    public string LangCode { get; set; } = null!;

    public string? Address { get; set; } 

    [JsonIgnore]
    public virtual Patient? Patient { get; set; }
}
