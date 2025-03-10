﻿using DomainModel.Entities;

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

    public string? CurrencyCode { get; set; }

    public short BookingStatusId { get; set; }
    
    public string StatusReason { get; set; } = string.Empty;

    public int? Price { get; set; }

    public byte DayNumber { get; set; }

    public DateTime? VisitingDate { get; set; }
    public string BookingReason { get; set; } = string.Empty;

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
            CurrencyCode = arg.CurrencyCode,
            BookingStatusId = arg.BookingStatusId,
            VisitingDate = arg.VisitingDate,//Value.ToUniversalTime()
            Price = arg.Price,
            DayNumber = arg.DayNumber,
            BookingReason = arg.BookingReason,
            StatusReason = arg.StatusReason,
            
        };

        return enity;
    }
}
