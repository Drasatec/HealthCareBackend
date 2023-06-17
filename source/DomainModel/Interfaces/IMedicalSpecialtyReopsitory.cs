using DomainModel.Entities.TranslationModels;
using DomainModel.Models.Floors;
using DomainModel.Models;
using DomainModel.Models.MedicalSpecialteis;

namespace DomainModel.Interfaces;

public interface IMedicalSpecialtyRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(MedicalSpecialtyDto dto, Stream? image = null);
    Task<PagedResponse<MedicalSpecialtyDto>?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page);
    Task<MedicalSpecialtyDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<MedicalSpecialtyDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<MedicalSpecialtyDto?>> Update(MedicalSpecialtyDto dto, int id, Stream? image = null);
}