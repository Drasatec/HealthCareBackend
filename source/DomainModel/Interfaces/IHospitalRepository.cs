﻿using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Hospitals;

namespace DomainModel.Interfaces;

public interface IHospitalRepository : IGenericRepository
{

    Task<ResponseId> CreateWithImage(HospitalDto dto, Stream? image = null);
    Task<Response<HospitalDto?>> UpdateHospital(HospitalDto dto, int id, Stream? image = null);
    Task<HospitalDto?> ReadHospitalById(int? Id, string? lang = null);
    Task<AllHospitalsDto?> ReadAllHospitals(string? status, string? lang, int? pageSize, int page = 1);
    Task<List<HospitalTranslation>> SearchByHospitalName(string name);
    Task<AllHospitalsDto?> SearchByHospitalNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = 10);
    Task<Response<string?>> UpdateAnImage(Stream image, int id);

    //Task<Response> DeleteHospitalById(int id, bool isDeleted);
    //Task<Response<HospitalDto?>> AddTranslations(List<HospitalTranslation> dto, int id);
    //Task<Response<HospitalDto?>> AddPhoneNumbers(List<HospitalPhoneNumber> dto, int id);
    //Task<Response> DeleteTranslat(int[] id);
    //Task<Response> DeletePhons(int[] ids);
}
