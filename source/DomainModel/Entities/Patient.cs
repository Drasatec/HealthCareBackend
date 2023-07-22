using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class Patient
{
    public int Id { get; set; }

    public string MedicalFileNumber { get; set; } = null!;

    public string PhoneNumber { get; set; } = string.Empty;

    //public string? Address { get; set; }  // delete this

    public byte? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public byte? MaritalStatus { get; set; }

    public byte? Ssn { get; set; } // delete this

    public byte? NationalId { get; set; }

    public string? BloodType { get; set; }
    
    public byte? Religion { get; set; }

    public byte? PatientStatus { get; set; }

    public string? Photo { get; set; }

    public int? SsntypeId { get; set; }

    public bool IsDeleted { get; set; }

    public int? ClientId { get; set; }

    public int? ClientGroupId { get; set; }

    public int? NationalityId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual HosClient? Client { get; set; }

    public virtual ClientGroup? ClientGroup { get; set; }

    public virtual Nationality? Nationality { get; set; }

    public virtual ICollection<PatientTranslation> PatientTranslations { get; set; } = new List<PatientTranslation>();

    public virtual Ssntype? Ssntype { get; set; }
}
