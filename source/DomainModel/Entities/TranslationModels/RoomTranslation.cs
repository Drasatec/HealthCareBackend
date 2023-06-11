using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class RoomTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int? RoomId { get; set; }

    public string? LangCode { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
    [JsonIgnore]
    public virtual HosRoom? Room { get; set; }
}
