using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Entities.Users;

namespace DomainModel.Entities;

public partial class Patient
{
    public int Id { get; set; }

    public string MedicalFileNumber { get; set; } = null!;

    public string PhoneNumber { get; set; } = string.Empty;

    public byte? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public byte? MaritalStatus { get; set; }

    public byte? Ssn { get; set; }

    public byte? NationalId { get; set; }

    public string? BloodType { get; set; }

    public byte? PatientStatus { get; set; }

    public string? Photo { get; set; }

    public byte? Religion { get; set; }

    public int? SsntypeId { get; set; }

    public bool IsDeleted { get; set; }

    public int? ClientId { get; set; }

    public int? ClientGroupId { get; set; }

    public int? NationalityId { get; set; }

    public string? UserId { get; set; } = string.Empty;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual HosClient? Client { get; set; }

    public virtual User? User { get; set; }

    public virtual ClientGroup? ClientGroup { get; set; }

    public virtual Nationality? Nationality { get; set; }

    public virtual ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();

    public virtual Ssntype? Ssntype { get; set; }
}
