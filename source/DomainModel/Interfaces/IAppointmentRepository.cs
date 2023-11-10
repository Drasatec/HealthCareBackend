using DomainModel.Models;
using DomainModel.Models.Bookings;
using DomainModel.Models.Common;

namespace DomainModel.Interfaces;

public interface IAppointmentRepository : IGenericRepository
{
    Task<ResponseLongId?> CreateAppointments(BookingRequestDto dto);
    Task<PagedResponse<BookingResponseDto>?> ReadAllAppointments(AppointmentFilterOptions filterOptions, PaginationOptions pageOptions, string? lang);
    Task<BookingResponseDto?> ReadAppointmentById(int Id, string? lang);
}
