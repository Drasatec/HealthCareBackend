using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.SettingsEntities;

namespace DomainModel.Entities.TranslationModels;

public partial class WorkingPeriodTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? WorkingPeriodId { get; set; }

    public string LangCode { get; set; } = null!;

    //public virtual Language? LangCodeNavigation { get; set; }
    [JsonIgnore]
    public virtual WorkingPeriod? WorkingPeriod { get; set; }
}
