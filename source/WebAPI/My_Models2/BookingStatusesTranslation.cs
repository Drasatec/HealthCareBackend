using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class BookingStatusesTranslation
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public short? BookingStatusId { get; set; }

    public string? LangCode { get; set; }

    public virtual BookingStatus? BookingStatus { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
