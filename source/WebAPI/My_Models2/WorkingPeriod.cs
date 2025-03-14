﻿using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class WorkingPeriod
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<DoctorWorkPeriod> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriod>();

    public virtual ICollection<WorkingPeriodTranslation> WorkingPeriodTranslations { get; set; } = new List<WorkingPeriodTranslation>();
}
