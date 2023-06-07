using System;
using System.Collections.Generic;

namespace DomainModel.Entities.TranslationModels;

public partial class FloorTranslation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? FloorId { get; set; }

    public string? LangCode { get; set; }

    public virtual HosFloor? Floor { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
