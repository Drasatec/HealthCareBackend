using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class PatientTranslation
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Occupation { get; set; }

    public string? Address { get; set; }

    public string LangCode { get; set; } = null!;

    public int? PatientId { get; set; }

    [JsonIgnore]
    public virtual Patient? Patient { get; set; }
}
