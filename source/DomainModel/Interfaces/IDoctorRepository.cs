
using DomainModel.Models;
using DomainModel.Models.Doctors;

namespace DomainModel.Interfaces;

public interface IDoctorRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(DoctorDto dto, Stream? image = null);
    Task<PagedResponse<DoctorDto>?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page);
    Task<DoctorDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<DoctorDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<DoctorDto?>> Update(DoctorDto dto, int id, Stream? image = null);
}