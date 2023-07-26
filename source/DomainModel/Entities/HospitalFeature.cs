using DomainModel.Entities.TranslationModels;
using System.Text.Json.Serialization;

namespace DomainModel.Entities;

public partial class HospitalFeature
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? HospitalId { get; set; }

    [JsonIgnore]
    public virtual Hospital? Hospital { get; set; }

    public virtual ICollection<HospitalFeatureTranslation> HospitalFeatureTranslations { get; set; } = new List<HospitalFeatureTranslation>();
}
