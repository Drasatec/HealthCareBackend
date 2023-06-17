using DomainModel.Models;
using DomainModel.Models.Dtos;

namespace DomainModel.Interfaces;

public interface IClinicRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(ClinicDto dto, Stream? image = null);
    Task<PagedResponse<ClinicDto>?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page);
    Task<ClinicDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<ClinicDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<ClinicDto?>> Update(ClinicDto dto, int id, Stream? image = null);
}