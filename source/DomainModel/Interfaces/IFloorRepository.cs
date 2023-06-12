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
    Task<AllFloorDto?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int page = 1);
    Task<FloorDto?> ReadById(int? Id, string? lang = null);
    Task<List<FloorTranslation>> SearchByName(string name, int? hosId);
    Task<AllFloorDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = 10);
    Task<Response<FloorDto?>> Update(FloorDto dto, int id, Stream? image = null);
}
