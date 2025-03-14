﻿using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class ReligionsTranslation
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public short? ReligionId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual Religion? Religion { get; set; }
}
