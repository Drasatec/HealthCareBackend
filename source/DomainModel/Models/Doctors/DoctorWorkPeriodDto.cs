using DomainModel.Entities.TranslationModels;
using DomainModel.Entities;

namespace DomainModel.Models.Doctors;
public class DoctorWorkPeriodDto
{
    public DoctorWorkPeriod? DoctorWorkPeriod { get; set; }

    public DoctorDto? Doctor { get; set; }

    //public ICollection<DoctorTranslation> DoctorTranslations { get; set; } = null!;


    public static implicit operator DoctorWorkPeriodDto(DoctorWorkPeriod arg)
    {
        if (arg == null)
            return null!;

        return new DoctorWorkPeriodDto
        {
            DoctorWorkPeriod = arg,
            Doctor = (DoctorDto)arg.Doctor,

        };
    }

    public static List<DoctorWorkPeriodDto> ToList(IEnumerable<DoctorWorkPeriod> enititeis)
    {
        var listDto = new List<DoctorWorkPeriodDto>(enititeis.Count());
        foreach (var entity in enititeis)
        {
            var dto = (DoctorWorkPeriodDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}