using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models.Dtos;

namespace DomainModel.Models.Patients;

public class PatientDto
{
    public int Id { get; set; }

    public string MedicalFileNumber { get; set; } = null!;

    public string PhoneNumber { get; set; } = string.Empty;

    //public string? Address { get; set; }

    public byte? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public byte? MaritalStatus { get; set; }

    //public byte? Ssn { get; set; }

    //public byte? NationalId { get; set; }

    public string? BloodType { get; set; }

    //public byte? PatientStatus { get; set; }

    public string? Photo { get; set; }

    //public int? SsntypeId { get; set; }

    //public bool IsDeleted { get; set; }

    //public int? ClientId { get; set; }

    //public int? ClientGroupId { get; set; }

    public int? NationalityId { get; set; }

    public ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();


    public static implicit operator PatientDto(Patient arg)
    {
        if (arg == null)
            return null!;

        return new PatientDto
        {
            Id = arg.Id,
            PatientTranslations = arg.PatientTranslations,
            NationalityId = arg.NationalityId,
            //Address = arg.Address,
            BirthDate = arg.BirthDate,
            MaritalStatus = arg.MaritalStatus,
            //Ssn = arg.Ssn,
           // NationalId = arg.NationalId,
            BloodType = arg.BloodType,
           // PatientStatus = arg.PatientStatus,
            Photo = arg.Photo,
           // ClientGroupId = arg.ClientGroupId,
           // ClientId = arg.ClientId,
            Gender = arg.Gender,
            //IsDeleted   = arg.IsDeleted,
            MedicalFileNumber   = arg.MedicalFileNumber,
            PhoneNumber = arg.PhoneNumber,
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
            //Address = arg.Address,
            BirthDate = arg.BirthDate,
            MaritalStatus = arg.MaritalStatus,
            //Ssn = arg.Ssn,
            //NationalId = arg.NationalId,
            BloodType = arg.BloodType,
            // PatientStatus = arg.PatientStatus,
            Photo = arg.Photo,
            // ClientGroupId = arg.ClientGroupId,
            // ClientId = arg.ClientId,
            Gender = arg.Gender,
            //IsDeleted   = arg.IsDeleted,
            MedicalFileNumber = arg.MedicalFileNumber,
            PhoneNumber = arg.PhoneNumber,
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
