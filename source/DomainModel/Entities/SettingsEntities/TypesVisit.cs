﻿using DomainModel.Entities.TranslationModels;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.SettingsEntities;

public partial class TypesVisit
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public DateTime? CreateOn { get; }

    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();


    public virtual ICollection<TypesVisitTranslation> TypesVisitTranslations { get; set; } = new List<TypesVisitTranslation>();
}
