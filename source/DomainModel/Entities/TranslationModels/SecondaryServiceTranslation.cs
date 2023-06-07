﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class SecondaryServiceTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? SecondaryServiceId { get; set; }

    public string? LangCode { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
    [JsonIgnore]
    public virtual SecondaryService? SecondaryService { get; set; }
}
