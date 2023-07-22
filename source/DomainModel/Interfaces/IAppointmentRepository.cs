using DomainModel.Models.Dtos;

namespace DomainModel.Interfaces;

public interface IAppointmentRepository : IGenericRepository
{
    Task<List<BookingDto?>> ReadAllAppointments(int? id, int? hosId, int? ClinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, string? lang);

}
