using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class ConfirmationOption
{
    public string Id { get; set; } = null!;

    public string? OptionName { get; set; }

    public bool Chosen { get; set; }
}
