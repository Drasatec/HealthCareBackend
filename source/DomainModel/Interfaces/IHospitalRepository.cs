using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Hospitals;

namespace DomainModel.Interfaces;

public interface IHospitalRepository : IGenericRepository
{

    Task<ResponseId> CreateWithImage(HospitalDto dto, Stream? image = null);
    Task<Response<HospitalDto?>> UpdateHospital(HospitalDto dto, int id, Stream? image = null);
    Task<HospitalDto?> ReadHospitalById(int? Id, string? lang = null);
    Task<PagedResponse<HospitalDto>?> ReadAllHospitals(string? status, string? lang, int? pageSize, int? page);
    Task<List<HospitalTranslation>> SearchByHospitalName(string name);
    Task<PagedResponse<HospitalDto>?> SearchByHospitalNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<string?>> UpdateAnImage(Stream image, int id);
    Task<Response<HospitalFeature?>> UpdateHospitalFeature(HospitalFeature entity, int id, Stream? image = null);
    Task<List<HospitalTranslation>> ReadAllHosNames(int? docId, bool? active, string lang, int? page, int? pageSize);

    //Task<Response> DeleteHospitalById(int id, bool isDeleted);
    //Task<Response<HospitalDto?>> AddTranslations(List<HospitalTranslation> dto, int id);
    //Task<Response<HospitalDto?>> AddPhoneNumbers(List<HospitalPhoneNumber> dto, int id);
    //Task<Response> DeleteTranslat(int[] id);
    //Task<Response> DeletePhons(int[] ids);
}
