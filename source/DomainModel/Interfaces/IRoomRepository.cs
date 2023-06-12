﻿using DomainModel.Entities.TranslationModels;
using DomainModel.Models.Floors;
using DomainModel.Models;
using DomainModel.Models.Rooms;

namespace DomainModel.Interfaces;

public interface IRoomRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(RoomDto dto, Stream? image = null);
    Task<AllRoomDto?> ReadAll(bool? isHosActive, string isActive, string? lang, int page = 1, int pageSize = 10);
    Task<RoomDto?> ReadById(int? Id, string? lang = null);
    Task<List<RoomTranslation>> SearchByName(string name);
    Task<AllRoomDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = 10);
    Task<Response<RoomDto?>> Update(RoomDto dto, int id, Stream? image = null);

}