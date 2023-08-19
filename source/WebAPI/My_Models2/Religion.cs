using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Religion
{
    public byte Id { get; set; }

    public virtual ICollection<ReligionsTranslation> ReligionsTranslations { get; set; } = new List<ReligionsTranslation>();
}
