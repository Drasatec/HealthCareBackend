﻿using DomainModel.Entities;

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

    public string? CurrencyCode { get; set; }

    public short? BookingStatusId { get; set; }

    public string? StatusReason { get; set; }

    public int? Price { get; set; }

    public DateTimeOffset? VisitingDate { get; set; }

    public DateTimeOffset? CreateOn { get; set; }

    public byte DayNumber { get; set; }

    public string? BookingReason { get; set; }
    

    public string? BookingStatus { get; set; }

    public string? Clinic { get; set; }

    public string? Currency { get; set; }

    public string Doctor { get; set; } = null!;

    public string Hospital { get; set; } = null!;

    public string? Patient { get; set; } 

    public string? PriceCategory { get; set; }

    public string Specialty { get; set; } = null!;

    public string TypeVisit { get; set; } = null!;

    public string WorkingPeriod { get; set; } = null!;


    public static implicit operator BookingResponseDto(Booking arg)
    {
        if (arg == null)
            return null!;

        return new BookingResponseDto
        {
            Id = arg.Id,
            Price = arg.Price,
            TypeVisitId = arg.TypeVisitId,
            PriceCategoryId = arg.PriceCategoryId,
            DoctorId = arg.DoctorId,
            BookingStatusId = arg.BookingStatusId,
            ClinicId = arg.ClinicId,
            CreateOn = arg.CreateOn,
            CurrencyCode = arg.CurrencyCode,
            HospitalId = arg.HospitalId,
            PatientId = arg.PatientId,
            SpecialtyId = arg.SpecialtyId,
            VisitingDate = arg.VisitingDate,
            WorkingPeriodId = arg.WorkingPeriodId,
            BookingNumber = arg.BookingNumber,
            DayNumber = arg.DayNumber,
            BookingReason = arg.BookingReason,
            StatusReason = arg.StatusReason,
        };
    }

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
