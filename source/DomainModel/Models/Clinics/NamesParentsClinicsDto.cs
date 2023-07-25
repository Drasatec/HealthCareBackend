
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


    public int HospitalName { get; set; }

    public int BuildName { get; set; }

    public int FloorName { get; set; }

    public int RoomName { get; set; }

    public int SpecialtyName { get; set; }
}
