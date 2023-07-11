using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class Gender
{
    public int Id { get; set; }

    public byte GenderNumber { get; set; }

    public string GenderName { get; set; } = null!;

    public string LangCode { get; set; } = null!;

}
