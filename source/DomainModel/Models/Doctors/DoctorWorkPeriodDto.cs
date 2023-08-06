using DomainModel.Entities.TranslationModels;
using DomainModel.Entities.DoctorEntities;

namespace DomainModel.Models.Doctors;
public class DoctorWorkPeriodDto
{
    public PeriodWorkDoctorClinicDto? doctorWorkPeriod { get; set; }

    //public DoctorWorkPeriod? DoctorWorkPeriod { get; set; }

    //public WorkingPeriod? WorkingPeriod { get; set; } = null!;

    public DoctorDto? Doctor { get; set; }

    //public ICollection<DoctorTranslation> DoctorTranslations { get; set; } = null!;


    public static implicit operator DoctorWorkPeriodDto(DoctorWorkPeriod arg)
    {
        if (arg == null)
            return null!;

        return new DoctorWorkPeriodDto
        {
            //DoctorWorkPeriod = arg,
            //WorkingPeriodId = arg.WorkingPeriodId,
            //doctorWorkPeriod = (PeriodWorkDoctorClinicDto)arg.WorkingPeriod,
            Doctor = (DoctorDto)arg.Doctor,
            //WorkingPeriod = arg.WorkingPeriod,

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