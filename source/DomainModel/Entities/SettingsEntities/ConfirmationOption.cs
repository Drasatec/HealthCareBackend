namespace DomainModel.Entities.SettingsEntities;
public partial class ConfirmationOption
{
    public string Id { get; set; } = null!;

    public string? OptionName { get; set; }

    public bool Chosen { get; set; }
}
