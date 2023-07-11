using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Buildings;

namespace DomainModel.Interfaces;

public interface IBuildingRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(BuildingDto dto, Stream? image = null);
    Task<Response<BuildingDto?>> Update(BuildingDto dto, int id, Stream? image = null);
    Task<BuildingDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<BuildingDto>?> ReadAll(int? parentId, bool? isHosActive, string? status, string? lang, int? pageSize, int? page);
    Task<PagedResponse<BuildingDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
}
