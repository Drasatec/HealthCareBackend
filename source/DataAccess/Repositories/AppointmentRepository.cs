using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Bookings;
using DomainModel.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AppointmentRepository : GenericRepository, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }

    public async Task<ResponseLongId?> CreateAppointments(BookingRequestDto dto)
    {
        var entity = (Booking)dto;
        try
        {
            ResponseLongId res;
            if (entity is not null)
            {
                if (entity.BookingStatusId < 1)
                {
                    entity.BookingStatusId = 1;
                }
                entity.BookingNumber = $"B{DateTime.Now.ToString("yyyyddMM")}{Context.Bookings.Count()}";

                entity.VisitingDate = entity.VisitingDate.HasValue ? entity.VisitingDate.Value.ToUniversalTime() : null;
                var result = await Context.Bookings.AddAsync(entity);
                var row = await Context.SaveChangesAsync();
                if (row > 0)
                {
                    res = new ResponseLongId(true, "created ", result.Entity.Id);
                }
                else
                    res = new ResponseLongId(false, "No row effected ", 0);

            }
            else
                res = new ResponseLongId(false, "enity == null ", 0); ;
            return res;


        }
        catch (Exception ex)
        {
            return new ResponseLongId(false, ex.Message + "____and____" + ex.InnerException?.Message, 0);
        }
    }

    public async Task<BookingResponseDto?> ReadAppointmentById(int Id, string? lang)
    {
        try
        {
            IQueryable<Booking> query = Context.Bookings;
            IQueryable<BookingResponseDto> result;

            query = query.Where(i => i.Id.Equals(Id));

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

                          join bstat in Context.BookingStatusesTranslations on h.BookingStatusId equals bstat.BookingStatusId
                          where bstat.LangCode == lang

                          select new BookingResponseDto
                          {
                              Id = h.Id,
                              BookingNumber = h.BookingNumber,
                              DoctorId = h.DoctorId,
                              WorkingPeriodId = h.WorkingPeriodId,
                              PatientId = h.PatientId,
                              ClinicId = h.ClinicId,
                              HospitalId = h.HospitalId,
                              TypeVisitId = h.TypeVisitId,
                              VisitingDate = h.VisitingDate,
                              SpecialtyId = h.SpecialtyId,
                              CurrencyCode = h.CurrencyCode,
                              BookingStatusId = h.BookingStatusId,
                              Price = h.Price,
                              PriceCategoryId = h.PriceCategoryId,
                              DayNumber = h.DayNumber,
                              BookingReason = h.BookingReason,
                              StatusReason = h.StatusReason,

                              Hospital = hos.Name,
                              Specialty = spec.Name,
                              Clinic = cli.Name,
                              Doctor = doc.FullName,
                              TypeVisit = tv.Name,
                              WorkingPeriod = wp.Name,
                              BookingStatus = bstat.StatusName,
                              Patient = (from pt in Context.PatientTranslations
                                         where pt.PatientId == h.PatientId
                                         where pt.LangCode == lang || pt.LangCode != lang && pt.LangCode == Constants.BaseLang
                                         select pt.FullName).FirstOrDefault(),
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
                              SpecialtyId = h.SpecialtyId,
                              CurrencyCode = h.CurrencyCode,
                              BookingStatusId = h.BookingStatusId,
                              PriceCategoryId = h.PriceCategoryId,
                              Price = h.Price,
                              VisitingDate = h.VisitingDate,
                              BookingNumber = h.BookingNumber,
                              BookingReason = h.BookingReason,
                              StatusReason = h.StatusReason,
                          });


            return await result.FirstOrDefaultAsync();
        }
        catch (Exception)
        {

            return null;
        }
    }

    public async Task<PagedResponse<BookingResponseDto>?> ReadAllAppointments(AppointmentFilterOptions filterOptions, PaginationOptions pageOptions, string? lang)
    {
        try
        {
            IQueryable<Booking> query = Context.Bookings;
            IQueryable<BookingResponseDto> result;

            if (filterOptions.Id.HasValue)
            {
                query = query.Where(i => i.Id.Equals(filterOptions.Id));
            }
            else
            {
                if (filterOptions.HospitalId.HasValue && filterOptions.HospitalId > 0)
                {
                    query = query.Where(d => d.HospitalId.Equals(filterOptions.HospitalId));
                }

                if (filterOptions.SpecialtyId.HasValue && filterOptions.SpecialtyId > 0)
                {
                    query = query.Where(t => t.SpecialtyId.Equals(filterOptions.SpecialtyId));
                }

                if (filterOptions.ClinicId.HasValue && filterOptions.ClinicId > 0)
                {
                    query = query.Where(t => t.ClinicId.Equals(filterOptions.ClinicId));
                }

                if (filterOptions.DoctorId.HasValue)
                {
                    query = query.Where(t => t.DoctorId.Equals(filterOptions.DoctorId));
                }

                if (filterOptions.TypeVisitId.HasValue)
                {
                    query = query.Where(t => t.TypeVisitId.Equals(filterOptions.TypeVisitId));
                }

                if (filterOptions.WorkingPeriodId.HasValue)
                {
                    query = query.Where(t => t.WorkingPeriodId.Equals(filterOptions.WorkingPeriodId));
                }

                if (filterOptions.PatientId.HasValue)
                {
                    query = query.Where(t => t.PatientId.Equals(filterOptions.PatientId));
                }

                if (filterOptions.BookingStatusId.HasValue)
                {
                    query = query.Where(t => t.BookingStatusId.Equals(filterOptions.BookingStatusId));
                }

                if (filterOptions.DayNumber.HasValue)
                {
                    query = query.Where(t => t.DayNumber.Equals(filterOptions.DayNumber));
                }

                if (filterOptions.bookingTime is not null && filterOptions.bookingTime == "past")
                {
                    query = query.Where(t => t.VisitingDate < DateTimeOffset.UtcNow);
                }

                if (filterOptions.bookingTime is not null && filterOptions.bookingTime == "future")
                {
                    query = query.Where(t => t.VisitingDate > DateTimeOffset.UtcNow);
                }

                if (filterOptions.StartDateTime is not null && filterOptions.EndDateTime is not null)
                {
                    if (filterOptions.StartDateTime > filterOptions.EndDateTime)
                        return null;
                    //query = query.Where(t => t.VisitingDate >= filterOptions.StartDateTime.Value.ToUniversalTime() && t.VisitingDate <= filterOptions.EndDateTime.Value.ToUniversalTime());
                }

                if (filterOptions.StartDateTime is not null)
                {
                    query = query.Where(t => t.VisitingDate >= filterOptions.StartDateTime.Value.ToUniversalTime());
                }

                if (filterOptions.EndDateTime is not null)
                {
                    query = query.Where(et => et.VisitingDate <= filterOptions.EndDateTime.Value.ToUniversalTime());
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

                          join bstat in Context.BookingStatusesTranslations on h.BookingStatusId equals bstat.BookingStatusId
                          where bstat.LangCode == lang

                          //join pati in Context.PatientTranslations on h.PatientId equals pati.PatientId
                          //where ( pati.LangCode == lang )|| (lang != Constants.BaseLang && pati.LangCode != lang && pati.LangCode == Constants.BaseLang)

                          select new BookingResponseDto
                          {
                              Id = h.Id,
                              BookingNumber = h.BookingNumber,
                              DoctorId = h.DoctorId,
                              WorkingPeriodId = h.WorkingPeriodId,
                              PatientId = h.PatientId,
                              ClinicId = h.ClinicId,
                              HospitalId = h.HospitalId,
                              TypeVisitId = h.TypeVisitId,
                              VisitingDate = h.VisitingDate,
                              SpecialtyId = h.SpecialtyId,
                              CurrencyCode = h.CurrencyCode,
                              BookingStatusId = h.BookingStatusId,
                              Price = h.Price,
                              PriceCategoryId = h.PriceCategoryId,
                              DayNumber = h.DayNumber,
                              BookingReason = h.BookingReason,
                              StatusReason = h.StatusReason,

                              Hospital = hos.Name,
                              Specialty = spec.Name,
                              Clinic = cli.Name,
                              Doctor = doc.FullName,
                              TypeVisit = tv.Name,
                              WorkingPeriod = wp.Name,
                              BookingStatus = bstat.StatusName,
                              Patient = (from pt in Context.PatientTranslations
                                         where pt.PatientId == h.PatientId
                                         where pt.LangCode == lang || pt.LangCode != lang && pt.LangCode == Constants.BaseLang
                                         select pt.FullName).FirstOrDefault(),
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
                              SpecialtyId = h.SpecialtyId,
                              CurrencyCode = h.CurrencyCode,
                              BookingStatusId = h.BookingStatusId,
                              PriceCategoryId = h.PriceCategoryId,
                              Price = h.Price,
                              VisitingDate = h.VisitingDate,
                              BookingNumber = h.BookingNumber,
                              BookingReason = h.BookingReason,
                              StatusReason = h.StatusReason,
                          });

            var totalCount = await result.CountAsync();

            GenericPagination(ref result, ref pageOptions, totalCount);

            var listDto = await result.OrderByDescending(h => h.Id)
                                     .ToListAsync();

            var all = new PagedResponse<BookingResponseDto>
            {
                Total = totalCount,
                Page = pageOptions.Page,
                PageSize = pageOptions.PageSize,
                Data = listDto
            };

            return all;
        }
        catch (Exception)
        {

            return null;
        }
    }
}
