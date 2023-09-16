using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Models.UsersV2;
public class PatientRegisterDto
{
    // Patients info
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public short? NationalityId { get; set; }

    public short? GenderId { get; set; }

    public short? MaritalStatusId { get; set; }

    public string? Address { get; set; }

    public string LangCode { get; set; } = null!;

    // user acount

    public string? PhoneNumber { get; set; }

    public string? CallingCode { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public static implicit operator Patient(PatientRegisterDto dto)
    {
        return new Patient()
        {
            Id = dto.Id,
            BirthDate = dto.BirthDate,
            NationalityId = dto.NationalityId,
            GenderId = dto.GenderId,
            MaritalStatusId = dto.MaritalStatusId,
            PatientTranslations = new List<PatientTranslation>()
            {
                new PatientTranslation()
                {
                    FullName = dto.FullName,
                    LangCode = dto.LangCode,
                    Address = dto.Address,
                }
            } 
            //UserAccount = new UserAccount()
            //{
            //    PhoneNumber = dto.PhoneNumber,
            //    CallingCode = dto.CallingCode,
            //    Email = dto.Email,
            //}
        };
    }
}
