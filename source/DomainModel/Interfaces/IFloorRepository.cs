using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Floors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces;

public interface IFloorRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(FloorDto dto, Stream? image = null);
    Task<PagedResponse<FloorDto>?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page);
    Task<FloorDto?> ReadById(int? Id, string? lang = null);
    Task<List<FloorTranslation>> SearchByName(string name, int? hosId);
    Task<PagedResponse<FloorDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<FloorDto?>> Update(FloorDto dto, int id, Stream? image = null);
}
