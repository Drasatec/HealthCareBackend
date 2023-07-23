using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class BookingStatus
{
    public short Id { get; set; }

    public DateTime? CreateOn { get;}

    public virtual ICollection<BookingStatusesTranslation> BookingStatusesTranslations { get; set; } = new List<BookingStatusesTranslation>();

    //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
