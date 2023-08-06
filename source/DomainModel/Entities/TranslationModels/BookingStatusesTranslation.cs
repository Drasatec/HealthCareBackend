using System.Text.Json.Serialization;
using DomainModel.Entities.SettingsEntities;

namespace DomainModel.Entities.TranslationModels;

public partial class BookingStatusesTranslation
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public short BookingStatusId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]

    public virtual BookingStatus? BookingStatus { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
