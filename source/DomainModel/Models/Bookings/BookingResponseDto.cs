using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models.Bookings;

public class BookingResponseDto
{
    public long Id { get; set; }

    public string BookingNumber { get; set; } = string.Empty;

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

    public byte DayNumber { get; set; }

    public DateTime? CreateOn { get; set; }
    public string? BookingReason { get; set; }

    public virtual string? BookingStatus { get; set; }

    public virtual string? Clinic { get; set; }

    public virtual string? Currency { get; set; }

    public virtual string Doctor { get; set; } = null!;

    public virtual string Hospital { get; set; } = null!;

    public virtual string Patient { get; set; } = null!;

    public virtual string? PriceCategory { get; set; }

    public virtual string Specialty { get; set; } = null!;

    public virtual string TypeVisit { get; set; } = null!;

    public virtual string WorkingPeriod { get; set; } = null!;


    //public static implicit operator BookingResponseDto(Booking arg)
    //{
    //    if (arg == null)
    //        return null!;

    //    return new BookingResponseDto
    //    {
    //        Id = arg.Id,
    //        Price = arg.Price,
    //        TypeVisitId = arg.TypeVisitId,
    //        PriceCategoryId = arg.PriceCategoryId,
    //        DoctorId = arg.DoctorId,
    //        BookingStatusId = arg.BookingStatusId,
    //        ClinicId = arg.ClinicId,
    //        CreateOn = arg.CreateOn,
    //        CurrencyId = arg.CurrencyId,
    //        HospitalId = arg.HospitalId,
    //        PatientId = arg.PatientId,
    //        SpecialtyId = arg.SpecialtyId,
    //        VisitingDate = arg.VisitingDate,
    //        WorkingPeriodId = arg.WorkingPeriodId,
    //        BookingNumber = arg.BookingNumber,
    //        DayNumber = arg.DayNumber,
    //    };
    //}

    //public static List<BookingResponseDto> ToList(IEnumerable<Booking> enList)
    //{
    //    var listDto = new List<BookingResponseDto>(enList.Count());
    //    foreach (var entity in enList)
    //    {
    //        var dto = (BookingResponseDto)entity;
    //        listDto.Add(dto);
    //    }
    //    return listDto;
    //}
}
