namespace WebAPI.My_Models2;

public partial class MaritalStatus
{
    public byte Id { get; set; }

    public virtual ICollection<MaritalStatusTranslation> MaritalStatusTranslations { get; set; } = new List<MaritalStatusTranslation>();
}
