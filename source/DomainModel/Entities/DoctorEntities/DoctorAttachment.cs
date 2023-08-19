namespace DomainModel.Entities.DoctorEntities;

public partial class DoctorAttachment
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? AttachFileName { get; set; }

    public DateTime? DateProduced { get; set; }

    public DateTime? CreateOn { get; set; }

    public int DoctorId { get; set; }

    // [JsonIgnore]
    // public virtual Doctor? Doctor { get; set; }
}
