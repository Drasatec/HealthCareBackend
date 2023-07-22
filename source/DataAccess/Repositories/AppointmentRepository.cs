using DataAccess.Contexts;
using DomainModel.Interfaces;
using DomainModel.Models.Bookings;
using DomainModel.Models.Doctors;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AppointmentRepository : GenericRepository, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }

    //join st in Context.MedicalSpecialtyTranslations on h.SpecialtyId equals st.MedicalSpecialtyId into specialtyTrans
    //               from st in specialtyTrans.DefaultIfEmpty()
    //               where st.LangCode == lang

    public async Task<List<BookingResponseDto?>> ReadAllAppointments(int? id, int? hosId,int? specialtyId, int? ClinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, short? bookStatusId ,string? lang)
    {
        var query = from h in Context.Bookings

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
                    };

        if (id.HasValue)
        {
            query = query.Where(i => i.Id.Equals(id));
        }
        else
        {
            if (hosId.HasValue)
            {
                query = query.Where(d => d.HospitalId.Equals(hosId));
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
        }
        return await query.ToListAsync();
    }
}
