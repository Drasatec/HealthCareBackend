using DomainModel.Models.Bookings;

namespace DomainModel.Interfaces;

public interface IAppointmentRepository : IGenericRepository
{
    Task<List<BookingResponseDto?>> ReadAllAppointments(int? id, int? hosId,int? specialtyId, int? ClinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, short? bookStatusId, string? lang);

}
