namespace DomainModel.Models.Dtos;

public class BookingDto
{
    public int Id { get; set; }

    public int? HospitalId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? WorkingPeriodId { get; set; }

    public DateTime? VisitingDate { get; set; }

    public int? TypeVisitId { get; set; }

    public int? ClinicId { get; set; }

    public DateTime? CreateOn { get; set; }

}
