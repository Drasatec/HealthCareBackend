﻿namespace DomainModel.Entities.SettingsEntities;
public class Weekday
{
    public int Id { get; set; }

    public byte DayNumber { get; set; }

    public string WeekdayName { get; set; } = null!;

    public string LangCode { get; set; } = null!;
}