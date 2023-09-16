using DomainModel.Entities.DoctorEntities;
using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Models.Doctors;
public class DoctorDtoV2
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public byte? Gender { get; set; }

    public string? Photo { get; set; }

    public short? DoctorsDegreeId { get; set; }

    public string? PhoneNumber { get; set; }

    public ICollection<DoctorTranslation> DoctorTranslations { get; set; } = new List<DoctorTranslation>();

    public ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();

    //public ICollection<DoctorsWorkHospital> DoctorsWorkHospitals { get; set; } = new List<DoctorsWorkHospital>();

    //public ICollection<DoctorWorkPeriod>? DoctorWorkPeriods = new List<DoctorWorkPeriod>();
}
