﻿using DomainModel.Entities.DoctorEntities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Doctors;

namespace DomainModel.Interfaces;

public interface IDoctorRepository : IGenericRepository
{
    Task<Response<DoctorDto>> CreateWithImage(DoctorDto dto, Stream? image = null);
    Task<PagedResponse<DoctorDto>?> ReadAll(int? hosId, int? specialtyId, byte? dayId , byte? genderId, short? degreeId, bool? appearance, string? status, string? lang, int? pageSize, int? page);
    Task<DoctorDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<DoctorDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string? lang, int? page, int? pageSize);
    Task<Response<DoctorDto?>> Update(DoctorDto dto, int id, Stream? image = null);


    Task<List<DoctorVisitPriceDto?>> ReadDoctorVisitPrices(int? id, int? docId, int? priceCategoryId, int? typeVisitId, int? price, string lang);
    Task<List<PeriodWorkDoctorClinicDto>> ReadDoctorWorkPeriod(int? id, int? docId, int? hosId, int? specialtyId, int? periodId, byte? day, string? lang);
    Task<Response<DoctorDto?>> UpdateAttachment(DoctorAttachment docAttachmnet, string ext, Stream? file = null);
    Task<PagedResponse<DoctorWorkPeriodDto>?> FindDoctor(int? hosId, int? specialtyId, int? docId, int? workingPeriodId, byte? day, short? doctorsDegreeId, byte? dender, int? page, int? pageSize, string? lang);
    Task<List<DoctorTranslation>> ReadDoctorsNames(int? hosId, int? specialtyId, string lang, int? page, int? pageSize);
    Task<PagedResponse<DoctorDtoV2>?> FindDoctor2(int? hosId, int? specialtyId, int? docId, int? workingPeriodId, byte? day, short? doctorsDegreeId, byte? gender, int? page, int? pageSize, string? lang);
    //Task<Response<DoctorAttachment>> AddDoctorAttachment(DoctorAttachment entity, Stream file);
}