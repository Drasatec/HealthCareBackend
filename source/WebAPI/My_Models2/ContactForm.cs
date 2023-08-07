using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class ContactForm
{
    public int Id { get; set; }

    public string SenderName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? ContactDate { get; set; }

    public int? HospitalId { get; set; }

    public virtual Hospital? Hospital { get; set; }
}
