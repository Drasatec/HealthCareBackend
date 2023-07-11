using DomainModel.Entities.TranslationModels;
using DomainModel.Models.Floors;
using DomainModel.Models;
using DomainModel.Models.Rooms;

namespace DomainModel.Interfaces;

public interface IRoomRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(RoomDto dto, Stream? image = null);
    Task<PagedResponse<RoomDto>?> ReadAll(int? roomTypeId, int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page);
    Task<RoomDto?> ReadById(int? Id, string? lang = null);
    Task<PagedResponse<RoomDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize);
    Task<Response<RoomDto?>> Update(RoomDto dto, int id, Stream? image = null);

}
