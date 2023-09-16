using DomainModel.Entities.Others;
using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Entities.Users;

namespace DomainModel.Entities;

public partial class Patient
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

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual HosClient? Client { get; set; }

    public virtual ClientGroup? ClientGroup { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual MaritalStatus? MaritalStatus { get; set; }

    public virtual Nationality? Nationality { get; set; }

    public virtual ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();

    public virtual Religion? Religion { get; set; }

    public virtual Ssntype? Ssntype { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
