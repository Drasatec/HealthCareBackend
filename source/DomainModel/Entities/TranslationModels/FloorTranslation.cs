using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.HospitalBody;

namespace DomainModel.Entities.TranslationModels;

public partial class FloorTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? FloorId { get; set; }

    public string LangCode { get; set; } = null!;
    [JsonIgnore]
    public virtual HosFloor? Floor { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
