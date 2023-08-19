using DomainModel.Entities.MedicalServices;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class SecondaryServiceTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? SecondaryServiceId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual SecondaryService? SecondaryService { get; set; }
}
