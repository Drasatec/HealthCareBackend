using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class TypesVisit
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public DateTime? CreateOn { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<TypesVisitTranslation> TypesVisitTranslations { get; set; } = new List<TypesVisitTranslation>();
}
