using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Buildings;

namespace DomainModel.Interfaces;

public interface IBuildingRepository : IGenericRepository
{
    Task<Response> CreateWithImage(BuildingDto dto, Stream? image = null);
    Task<AllBuildingDto?> ReadAll(bool? isHosActive, string isActive, string? lang, int page = 1, int pageSize = 10);


    Task<BuildingDto?> ReadById(int? Id, string? lang = null);



    Task<List<BuildingTranslation>> SearchByName(string name);
    Task<AllBuildingDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = 10);
    Task<Response<BuildingDto?>> UpdateHospital(BuildingDto dto, int id, Stream? image = null);
}
