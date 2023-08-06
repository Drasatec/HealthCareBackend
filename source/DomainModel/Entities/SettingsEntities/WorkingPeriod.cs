using System;
using System.Collections.Generic;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities.SettingsEntities;

public partial class WorkingPeriod
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();


    public virtual ICollection<WorkingPeriodTranslation> WorkingPeriodTranslations { get; set; } = new List<WorkingPeriodTranslation>();

    //public virtual ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();

}
