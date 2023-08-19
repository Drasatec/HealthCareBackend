namespace DomainModel.Entities.SettingsEntities;

public partial class Gender
{
    public int Id { get; set; }

    public byte GenderNumber { get; set; }

    public string GenderName { get; set; } = null!;

    public string LangCode { get; set; } = null!;

}
