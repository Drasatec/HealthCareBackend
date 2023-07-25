
namespace DomainModel.Models.Clinics;
public class NamesParentsClinicsDto
{
    public long BookingId { get; set; }
    
    public int ClinicId { get; set; }

    public short? WorkingHours { get; set; }

    public int HospitalId { get; set; }

    public int BuildId { get; set; }

    public int FloorId { get; set; }

    public int RoomId { get; set; }

    public int SpecialtyId { get; set; }


    public string HospitalName { get; set; } = string.Empty;

    public string BuildName { get; set; } = string.Empty;

    public string FloorName { get; set; } = string.Empty;

    public string RoomName { get; set; } = string.Empty;

    public string SpecialtyName { get; set; } = string.Empty;
}
