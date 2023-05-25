using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class PatientTranslation
{
    public string? FullName { get; set; }

    public string? Religion { get; set; }

    public string? Occupation { get; set; }

    public string? Employer { get; set; }

    public byte? RelationshipClient { get; set; }

    public int? NationalityId { get; set; }

    public int PatientId { get; set; }

    public string LangCode { get; set; } = null!;

    public virtual Language LangCodeNavigation { get; set; } = null!;

    public virtual Nationality? Nationality { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
