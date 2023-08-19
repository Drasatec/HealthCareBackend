using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class BookingStatus
{
    public short Id { get; set; }

    public DateTime? CreateOn { get; }

    public virtual ICollection<BookingStatusesTranslation> BookingStatusesTranslations { get; set; } = new List<BookingStatusesTranslation>();
}
