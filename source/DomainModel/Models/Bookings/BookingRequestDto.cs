using DomainModel.Entities;

namespace DomainModel.Models.Bookings;

public class BookingRequestDto
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int HospitalId { get; set; }

    public int SpecialtyId { get; set; }

    public int DoctorId { get; set; }

    public int WorkingPeriodId { get; set; }

    public int TypeVisitId { get; set; }

    public int? ClinicId { get; set; }

    public int? PriceCategoryId { get; set; }

    public int? CurrencyId { get; set; }

    public short? BookingStatusId { get; set; }

    public int? Price { get; set; }

    public DateTime? VisitingDate { get; set; }

    public static implicit operator Booking(BookingRequestDto arg)
    {
        var enity = new Booking
        {
            Id = arg.Id,
            Price = arg.Price,
            TypeVisitId = arg.TypeVisitId,
            PriceCategoryId = arg.PriceCategoryId,
            DoctorId = arg.DoctorId,
            BookingStatusId = arg.BookingStatusId,
            ClinicId = arg.ClinicId,
            CurrencyId = arg.CurrencyId,
            HospitalId = arg.HospitalId,
            PatientId = arg.PatientId,
            SpecialtyId = arg.SpecialtyId,
            VisitingDate = arg.VisitingDate,
            WorkingPeriodId = arg.WorkingPeriodId,
        };

        return enity;
    }
}
