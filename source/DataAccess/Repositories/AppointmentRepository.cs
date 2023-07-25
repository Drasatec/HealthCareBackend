using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Bookings;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AppointmentRepository : GenericRepository, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }


    public async Task<ResponseLongId> CreateAppointments(BookingRequestDto dto)
    {
        var entity = (Booking)dto;

        try
        {
            var result = await Context.Bookings.AddAsync(entity);
            entity.BookingNumber = $"B{DateTime.Now.ToString("yyyyddMM")} {Context.Bookings.Count()} ";

            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new ResponseLongId(true, "created ", result.Entity.Id);
            }
            return new ResponseLongId(false, "No row effected ", 0);
        }
        catch (Exception ex)
        {
            return new ResponseLongId(false, ex.Message + "____and____" + ex.InnerException?.Message, 0);
        }
    }

    public async Task<PagedResponse<BookingResponseDto>?> ReadAllAppointments(long? id, int? hosId, int? specialtyId, int? ClinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, short? bookStatusId, byte? dayNumber, string? lang, int? page, int? pageSize)
    {
        IQueryable<Booking> query = Context.Bookings;
        IQueryable<BookingResponseDto> result;

        if (id.HasValue)
        {
            query = query.Where(i => i.Id.Equals(id));
        }
        else
        {
            if (specialtyId.HasValue)
            {
                query = query.Where(d => d.SpecialtyId.Equals(specialtyId));
            }

            if (ClinicId.HasValue)
            {
                query = query.Where(t => t.ClinicId.Equals(ClinicId));
            }

            if (ClinicId.HasValue)
            {
                query = query.Where(t => t.ClinicId.Equals(ClinicId));
            }

            if (docId.HasValue)
            {
                query = query.Where(t => t.DoctorId.Equals(docId));
            }

            if (typeVisitId.HasValue)
            {
                query = query.Where(t => t.TypeVisitId.Equals(typeVisitId));
            }

            if (workingPeriodId.HasValue)
            {
                query = query.Where(t => t.WorkingPeriodId.Equals(workingPeriodId));
            }

            if (patientId.HasValue)
            {
                query = query.Where(t => t.PatientId.Equals(patientId));
            }

            if (bookStatusId.HasValue)
            {
                query = query.Where(t => t.BookingStatusId.Equals(bookStatusId));
            }

            if (dayNumber.HasValue)
            {
                query = query.Where(t => t.DayNumber.Equals(dayNumber));
            }
        }
        if (lang != null)
            result = (from h in query

                      join hos in Context.HospitalTranslations on h.HospitalId equals hos.HospitalId
                      where hos.LangCode == lang

                      join spec in Context.MedicalSpecialtyTranslations on h.SpecialtyId equals spec.MedicalSpecialtyId
                      where spec.LangCode == lang

                      join cli in Context.ClinicTranslations on h.ClinicId equals cli.ClinicId
                      where cli.LangCode == lang

                      join doc in Context.DoctorTranslations on h.DoctorId equals doc.DoctorId
                      where doc.LangCode == lang

                      join tv in Context.TypesVisitTranslations on h.TypeVisitId equals tv.TypeVisitId
                      where tv.LangCode == lang

                      join wp in Context.WorkingPeriodTranslations on h.WorkingPeriodId equals wp.WorkingPeriodId
                      where wp.LangCode == lang

                      join pati in Context.WorkingPeriodTranslations on h.WorkingPeriodId equals pati.WorkingPeriodId
                      where pati.LangCode == lang

                      join bstat in Context.BookingStatusesTranslations on h.BookingStatusId equals bstat.BookingStatusId
                      where bstat.LangCode == lang

                      select new BookingResponseDto
                      {
                          Id = h.Id,
                          DoctorId = h.DoctorId,
                          WorkingPeriodId = h.WorkingPeriodId,
                          PatientId = h.PatientId,
                          ClinicId = h.ClinicId,
                          HospitalId = h.HospitalId,
                          TypeVisitId = h.TypeVisitId,
                          VisitingDate = h.VisitingDate,
                          SpecialtyId = h.SpecialtyId,
                          CurrencyId = h.CurrencyId,
                          BookingStatusId = h.BookingStatusId,
                          Price = h.Price,
                          PriceCategoryId = h.PriceCategoryId,

                          Hospital = hos.Name,
                          Specialty = spec.Name,
                          Clinic = cli.Name,
                          Doctor = doc.FullName,
                          TypeVisit = tv.Name,
                          WorkingPeriod = wp.Name,
                          Patient = pati.Name,
                          BookingStatus = bstat.StatusName,

                          CreateOn = h.CreateOn,
                      });
        else
            result = (from h in query
                      select new BookingResponseDto
                      {
                          Id = h.Id,
                          DoctorId = h.DoctorId,
                          WorkingPeriodId = h.WorkingPeriodId,
                          PatientId = h.PatientId,
                          ClinicId = h.ClinicId,
                          HospitalId = h.HospitalId,
                          TypeVisitId = h.TypeVisitId,
                          VisitingDate = h.VisitingDate,
                          SpecialtyId = h.SpecialtyId,
                          CurrencyId = h.CurrencyId,
                          BookingStatusId = h.BookingStatusId,
                          Price = h.Price,
                          PriceCategoryId = h.PriceCategoryId,
                      });

        var totalCount = await query.CountAsync();

        GenericPagination(ref query, ref pageSize, ref page, totalCount);

        var listDto = await result.OrderByDescending(h => h.Id)
                                 .ToListAsync();

        var all = new PagedResponse<BookingResponseDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = listDto
        };

        return all;
    }
}
