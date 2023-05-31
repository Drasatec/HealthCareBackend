using DomainModel.Entities;
using DomainModel.Models;
using DomainModel.Models.Hospitals;

namespace DomainModel.Interfaces;

public interface IHospitalRepository : IGenericRepository<HospitalDto>
{

    Task<HospitalDto?> CreateWithImage(HospitalDto dto, Stream? image = null);
    Task<Response<HospitalDto?>> AddTranslations(List<HospitalTranslation> dto, int id);
    Task<HospitalDto?> ReadHospitalById(int Id, string? lang = null);
    Task<Response<HospitalDto?>> UpdateHospital(HospitalDto dto, int id, Stream? image = null);
    Task<Response<string?>> UpdateAnImage(Stream image, int id);
    Task<AllHospitalsDto?> ReadAllHospitals(string isActive, string? lang, int page = 1, int pageSize = 10);
    Task<Response> DeleteHospitalById(int id, bool isDeleted);
    Task<Response<List<HospitalTranslation>>> SearchByName(string name);
    Task<Response<List<HospitalDto>>> SearchByCodeNumber(string codeNumber, string lang);
    Task<Response<HospitalDto?>> AddPhoneNumbers(List<HospitalPhoneNumber> dto, int id);
    Task<Response> DeleteTranslat(int[] id);
    Task<Response> DeletePhons(int[] ids);
}
