using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class BookingStatus
{
    public short Id { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual ICollection<BookingStatusesTranslation> BookingStatusesTranslations { get; set; } = new List<BookingStatusesTranslation>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
