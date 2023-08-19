namespace DomainModel.Entities.SettingsEntities;
public partial class ConfirmationOption
{
    public string Code { get; set; } = null!;

    public string OptionName { get; set; } = string.Empty;

    public bool Chosen { get; set; }
}
