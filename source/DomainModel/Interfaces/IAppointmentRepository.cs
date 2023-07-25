using DomainModel.Models;
using DomainModel.Models.Bookings;

namespace DomainModel.Interfaces;

public interface IAppointmentRepository : IGenericRepository
{
    Task<ResponseLongId?> CreateAppointments(BookingRequestDto dto);
    Task<PagedResponse<BookingResponseDto>?> ReadAllAppointments(long? id, int? hosId,int? specialtyId, int? clinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, short? bookStatusId, byte? dayNumber, string? lang, int? page, int? pageSize);

}
