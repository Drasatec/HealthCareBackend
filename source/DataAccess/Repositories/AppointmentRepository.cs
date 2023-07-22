using DataAccess.Contexts;
using DomainModel.Interfaces;
using DomainModel.Models.Doctors;
using DomainModel.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AppointmentRepository : GenericRepository, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }

    public async Task<List<BookingDto?>> ReadAllAppointments(int? id, int? hosId, int? ClinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, string? lang)
    {
        var query = from h in Context.Bookings

                    join hos in Context.HospitalTranslations on h.HospitalId equals hos.HospitalId
                    where hos.LangCode == lang

                    join pc in Context.ClinicTranslations on h.ClinicId equals pc.ClinicId
                    where pc.LangCode == lang

                    join dt in Context.DoctorTranslations on h.DoctorId equals dt.DoctorId
                    where dt.LangCode == lang

                    join tv in Context.TypesVisitTranslations on h.TypeVisitId equals tv.TypeVisitId
                    where tv.LangCode == lang

                    join wp in Context.WorkingPeriodTranslations on h.WorkingPeriodId equals wp.WorkingPeriodId
                    where wp.LangCode == lang

                    join st in Context.SpecialtyTranslations on h.SpecialtyId equals st.SpecialtyId into specialtyTrans
                    from st in specialtyTrans.DefaultIfEmpty()
                    where st.LangCode == lang

                    select new BookingDto
                    {
                        Id = h.Id,
                        DoctorId = h.DoctorId,
                        WorkingPeriodId= h.WorkingPeriodId,
                        PatientId= h.PatientId,
                        ClinicId    = h.ClinicId,
                        HospitalId = h.HospitalId,
                        TypeVisitId = h.TypeVisitId,
                        VisitingDate = h.VisitingDate,
                        CreateOn = h.CreateOn,
                    };

        if (id.HasValue)
        {
            query = query.Where(i => i.Id.Equals(id));
        }

        if (docId.HasValue)
        {
            query = query.Where(d => d.DoctorId.Equals(docId));
        }

        if (typeVisitId.HasValue)
        {
            query = query.Where(t => t.TypeVisitId.Equals(typeVisitId));
        }


        return await query.ToListAsync();
    }
}
