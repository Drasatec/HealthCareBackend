using System.Text.Json.Serialization;

namespace DomainModel.Entities;
public class Weekday
{
    public int Id { get; set; }

    public byte? DayNumber { get; set; } = null!;

    public string WeekdayName { get; set; } = null!;

    public string LangCode { get; set; } = null!;
}
