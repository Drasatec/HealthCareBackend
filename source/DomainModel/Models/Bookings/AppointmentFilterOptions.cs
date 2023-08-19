namespace DomainModel.Models.Bookings;
public class AppointmentFilterOptions
{
    public long? Id { get; set; }
    public int? HospitalId { get; set; }
    public int? SpecialtyId { get; set; }
    public int? ClinicId { get; set; }
    public int? DoctorId { get; set; }
    public int? TypeVisitId { get; set; }
    public int? WorkingPeriodId { get; set; }
    public int? PatientId { get; set; }
    public short? BookingStatusId { get; set; }
    public byte? DayNumber { get; set; }
    public string? bookingTime { get; set; }
}
