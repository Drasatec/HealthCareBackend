using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Entities;

public partial class HospitalTranslation
{
    
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Address { get; set; }

    public int HospitalId { get; set; }

    public string LangCode { get; set; } = null!;

    //[JsonIgnore]
    //public virtual Hospital? Hospital { get; set; } = null!;
    [JsonIgnore]
    public virtual Language? LangCodeNavigation { get; set; }
}
