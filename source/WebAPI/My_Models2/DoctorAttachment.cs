using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class DoctorAttachment
{
    public int Id { get; set; }

    public string? AttachFileName { get; set; }

    public string? Title { get; set; }

    public DateTime? DateProduced { get; set; }

    public DateTime? CreateOn { get; set; }

    public int DoctorId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}
