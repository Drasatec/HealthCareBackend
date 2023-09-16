using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Models.Patients;

public class PatientDto
{
    public int Id { get; set; }

    public DateTime? BirthDate { get; set; }

    public byte? NationalId { get; set; }

    public byte? Ssn { get; set; }

    public string? BloodType { get; set; }

    public byte? PatientStatus { get; set; }

    public string? Photo { get; set; }

    public int? SsntypeId { get; set; }

    public bool IsDeleted { get; set; }

    public int? ClientId { get; set; }

    public int? ClientGroupId { get; set; }

    public int? NationalityId { get; set; }

    public short? GenderId { get; set; }

    public short? ReligionId { get; set; }

    public short? MaritalStatusId { get; set; }


    public ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();


    public static implicit operator PatientDto(Patient? arg)
    {
        if (arg == null)
            return null!;

        return new PatientDto
        {
            Id = arg.Id,
            PatientTranslations = arg.PatientTranslations,
            NationalityId = arg.NationalityId,
            BirthDate = arg.BirthDate,
            MaritalStatusId = arg.MaritalStatusId,
            BloodType = arg.BloodType,
            Photo = arg.Photo,
            GenderId = arg.GenderId,
            PatientStatus = arg.PatientStatus,

            //Ssn = arg.Ssn,
           // NationalId = arg.NationalId,
           // ClientGroupId = arg.ClientGroupId,
           // ClientId = arg.ClientId,
            //IsDeleted   = arg.IsDeleted,
            //SsntypeId = arg.SsntypeId
        };
    }

    public static implicit operator Patient(PatientDto arg)
    {
        var listTranc = new List<PatientTranslation>();
        Patient entity;
        if (arg.PatientTranslations != null)
            foreach (var item in arg.PatientTranslations)
            {
                item.PatientId = arg.Id;
                listTranc.Add(item);
            }

        entity = new Patient
        {
            Id = arg.Id,
            PatientTranslations = listTranc,
            NationalityId = arg.NationalityId,
            BirthDate = arg.BirthDate,
            MaritalStatusId = arg.MaritalStatusId,
            BloodType = arg.BloodType,
            Photo = arg.Photo,
            GenderId = arg.GenderId,

            //Address = arg.Address,
            //Ssn = arg.Ssn,
            //NationalId = arg.NationalId,
            // PatientStatus = arg.PatientStatus,
            // ClientGroupId = arg.ClientGroupId,
            // ClientId = arg.ClientId,
            //IsDeleted   = arg.IsDeleted,
            //SsntypeId = arg.SsntypeId

        };

        return entity;
    }

    public static List<PatientDto> ToList(IEnumerable<Patient> entities)
    {
        var listDto = new List<PatientDto>(entities.Count());
        foreach (var entity in entities)
        {
            var dto = (PatientDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }
}
