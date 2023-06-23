using System;
using System.Collections.Generic;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class WorkingPeriod
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    // public virtual ICollection<PeriodWorkDoctorClinic> PeriodWorkDoctorClinics { get; set; } = new List<PeriodWorkDoctorClinic>();

    public virtual ICollection<WorkingPeriodTranslation> WorkingPeriodTranslations { get; set; } = new List<WorkingPeriodTranslation>();
}
