﻿using DomainModel.Models;
using DomainModel.Models.Clinics;

namespace DomainModel.Interfaces;

public interface IClinicRepository : IGenericRepository
{
    Task<NamesParentsClinicsDto?> ClinicByIdWithParentsNames(int? id, string? lang = null);
    Task<ResponseId> CreateWithImage(ClinicDto dto, Stream? image = null);
    Task<PagedResponse<ClinicDto>?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page);
    Task<ClinicDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<ClinicDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<ClinicDto?>> Update(ClinicDto dto, int id, Stream? image = null);
}