namespace DomainModel.Entities.HosInfo;

public partial class HospitalPhoneNumber
{
    public int Id { get; set; }

    public string? TelephoneNumber { get; set; }

    public int? HospitalId { get; set; }

}
