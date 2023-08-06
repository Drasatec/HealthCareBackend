using DomainModel.Models;
using DomainModel.Models.Patients;

namespace DomainModel.Interfaces;

public interface IPatientRepository : IGenericRepository
{
    Task<ResponseId> CreateFromPatient(PatientDto dto, string userId);
    Task<ResponseId> CreateWithImage(PatientDto dto, Stream? image = null);
    Task<string?> FindByUserId(string userId);
    Task<PagedResponse<PatientDto>?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page);
    Task<PatientDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<PatientDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<PatientDto?>> Update(PatientDto dto, int id, Stream? image = null);
}
