using DataAccess.Contexts;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.DashboardStatisticsModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class StatisticsRepository : GenericRepository, IStatisticsRepository
{
    public StatisticsRepository(AppDbContext context) : base(context) { }

    public async Task<Response<EntityCounts>?> FetchCSDPCounts()
    {
        try
        {
            EntityCounts entityCounts = new EntityCounts();
            entityCounts.Clinics = await Context.Clinics.CountAsync(f => f.IsDeleted == false);
            entityCounts.Specialties = await Context.MedicalSpecialties.CountAsync(f => f.IsDeleted == false);
            entityCounts.Doctors = await Context.Doctors.CountAsync(f => f.IsDeleted == false);
            entityCounts.Patients = await Context.Patients.CountAsync(f => f.IsDeleted == false);
            return new Response<EntityCounts>(true, null, entityCounts);

        }
        catch (Exception ex)
        {
            return new Response<EntityCounts>(false, ex.Message + "____and____" + ex.InnerException?.Message);
        }
    }
    public async Task<Response<NumberBookingsYear>?> BookingStatistics(int? year)
    {
        try
        {
            NumberBookingsYear months = new NumberBookingsYear();
            months.Month01 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 1  && c1.BookingStatusId == 4);
            months.Month02 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 2  && c1.BookingStatusId == 4);
            months.Month03 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 3  && c1.BookingStatusId == 4);
            months.Month04 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 4  && c1.BookingStatusId == 4);
            months.Month05 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 5  && c1.BookingStatusId == 4);
            months.Month06 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 6  && c1.BookingStatusId == 4);
            months.Month07 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 7  && c1.BookingStatusId == 4);
            months.Month08 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 8  && c1.BookingStatusId == 4);
            months.Month09 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 9  && c1.BookingStatusId == 4);
            months.Month10 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 10 && c1.BookingStatusId == 4);
            months.Month11 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 11 && c1.BookingStatusId == 4);
            months.Month12 = await Context.Bookings.CountAsync(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 12 && c1.BookingStatusId == 4);

            //var months = await (from booking in Context.Bookings
            //                    let m1 = Context.Bookings.Count(c1 => c1.VisitingDate.Value.Year == year && c1.VisitingDate.Value.Month == 1 && c1.TypeVisitId == 4)
            //                    select new NumberBookingsYear()
            //                    {
            //                        Month1 = m1,
            //                        Month2 = m1,
            //                        Month3 = m1,
            //                        Month4 = m1,
            //                        Month5 = m1,
            //                        Month6 = m1,
            //                        Month7 = m1,
            //                    }).ToListAsync();
            if (months != null)
                return new Response<NumberBookingsYear>(true, null, months);
            else
                return new Response<NumberBookingsYear>(true, "null", null);
        }
        catch (Exception ex)
        {
            return new Response<NumberBookingsYear>(false, ex.Message + "____and____" + ex.InnerException?.Message);
        }
    }
}
