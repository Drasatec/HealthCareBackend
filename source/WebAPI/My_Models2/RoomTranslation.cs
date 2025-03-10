﻿using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class RoomTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? RoomId { get; set; }

    public string? LangCode { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }

    public virtual HosRoom? Room { get; set; }
}
