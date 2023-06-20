using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class RoomType
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public DateTime? CreateOn { get;}

    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public virtual ICollection<HosRoom> HosRooms { get; set; } = new List<HosRoom>();

    public virtual ICollection<RoomTypeTranslation> RoomTypeTranslations { get; set; } = new List<RoomTypeTranslation>();
}
