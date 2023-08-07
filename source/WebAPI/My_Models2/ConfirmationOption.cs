using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class ConfirmationOption
{
    public string Id { get; set; } = null!;

    public string? OptionName { get; set; }

    public bool Chosen { get; set; }
}
