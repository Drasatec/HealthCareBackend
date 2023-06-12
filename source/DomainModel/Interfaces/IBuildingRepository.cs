using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Buildings;

namespace DomainModel.Interfaces;

public interface IBuildingRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(BuildingDto dto, Stream? image = null);
    Task<Response<BuildingDto?>> Update(BuildingDto dto, int id, Stream? image = null);
    Task<BuildingDto?> ReadById(int? Id, string? lang = null);
    Task<AllBuildingDto?> ReadAll(int? baseId, bool? isHosActive, string? status, string? lang, int? pageSize, int page = 1);
    Task<List<BuildingTranslation>> SearchByName(string name, int? hosId);
    Task<AllBuildingDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = 10);
}
