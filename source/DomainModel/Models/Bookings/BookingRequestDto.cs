using DomainModel.Entities;

namespace DomainModel.Models.Bookings;

public class BookingRequestDto
{
    public long Id { get; set; }

    //public long BookingNumber { get; set; }

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

    public byte DayNumber { get; set; }

    public DateTime? VisitingDate { get; set; }

    public static implicit operator Booking(BookingRequestDto arg)
    {
        var enity = new Booking
        {
            Id = arg.Id,
            PatientId = arg.PatientId,
            HospitalId = arg.HospitalId,
            SpecialtyId = arg.SpecialtyId,
            DoctorId = arg.DoctorId,
            WorkingPeriodId = arg.WorkingPeriodId,
            TypeVisitId = arg.TypeVisitId,
            ClinicId = arg.ClinicId,
            PriceCategoryId = arg.PriceCategoryId,
            CurrencyId = arg.CurrencyId,
            BookingStatusId = arg.BookingStatusId,
            VisitingDate = arg.VisitingDate,
            Price = arg.Price,
        };

        return enity;
    }
}
