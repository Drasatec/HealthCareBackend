using DomainModel.Entities.TranslationModels;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.DoctorEntities;

public partial class DoctorsDegree
{
    public short Id { get; set; }
    public DateTime? CreateOn { get; set; }

    [JsonIgnore]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<DoctorsDegreesTranslation> DoctorsDegreesTranslations { get; set; } = new List<DoctorsDegreesTranslation>();
}
