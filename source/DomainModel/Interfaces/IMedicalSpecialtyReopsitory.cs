using DomainModel.Entities.TranslationModels;
using DomainModel.Models.Floors;
using DomainModel.Models;
using DomainModel.Models.MedicalSpecialteis;

namespace DomainModel.Interfaces;

public interface IMedicalSpecialtyRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(MedicalSpecialtyDto dto, Stream? image = null);
    Task<AllMedicalSpecialtyDto?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int page = 1);
    Task<MedicalSpecialtyDto?> ReadById(int? Id, string? lang = null);
    Task<List<MedicalSpecialtyTranslation>> SearchByName(string name, int? hosId);
    Task<AllMedicalSpecialtyDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = 10);
    Task<Response<MedicalSpecialtyDto?>> Update(MedicalSpecialtyDto dto, int id, Stream? image = null);
}