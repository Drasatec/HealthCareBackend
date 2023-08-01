using DomainModel.Entities;
namespace DomainModel.Models.Doctors;

public class PeriodWorkDoctorClinicDto
{
    public int Id { get; set; }

    public int? HospitalId { get; set; }

    public int? SpecialtyId { get; set; }


    public int ClinicId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public byte? OnDay { get; set; }


    public string? Hospital { get; set; }

    public string? Clinic { get; set; }

    public string? Doctor { get; set; }

    public string? WorkingPeriod { get; set; }

    public string? DayName { get; set; }



    public static implicit operator PeriodWorkDoctorClinicDto(DoctorWorkPeriod arg)
    {
        if (arg == null)
            return null!;

        return new PeriodWorkDoctorClinicDto
        {
            Id = arg.Id,
            DoctorId = arg.DoctorId,
            ClinicId = arg.ClinicId,
            HospitalId = arg.HospitalId,
            SpecialtyId = arg.SpecialtyId,
            WorkingPeriodId = arg.WorkingPeriodId,
        };
    }

    //public static implicit operator PeriodWorkDoctorClinic(PeriodWorkDoctorClinicDto arg)
    //{
    //    var enity = new PeriodWorkDoctorClinic
    //    {
    //        Id = arg.Id,
    //        DoctorId = arg.DoctorId,
    //    };

    //    return enity;
    //}

    public static List<PeriodWorkDoctorClinicDto> ToList(IEnumerable<DoctorWorkPeriod> enList)
    {
        var listDto = new List<PeriodWorkDoctorClinicDto>(enList.Count());
        foreach (var entity in enList)
        {
            var dto = (PeriodWorkDoctorClinicDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }
}
