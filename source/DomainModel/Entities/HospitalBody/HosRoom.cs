using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.TranslationModels;
namespace DomainModel.Entities.HospitalBody;

public partial class HosRoom
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public string? Kind { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int HospitalId { get; set; }

    public int BuildId { get; set; }

    public int FloorId { get; set; }

    public int RoomTypeId { get; set; }

    public virtual HosBuilding Build { get; set; } = null!;

    public virtual HosFloor Floor { get; set; } = null!;

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual RoomType RoomType { get; set; } = null!;

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<RoomTranslation> RoomTranslations { get; set; } = new List<RoomTranslation>();
}
