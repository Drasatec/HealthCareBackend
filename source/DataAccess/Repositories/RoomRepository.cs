using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Dtos;
using DomainModel.Models.Floors;
using DomainModel.Models.MedicalSpecialteis;
using DomainModel.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Repositories;

public class RoomRepository : GenericRepository, IRoomRepository
{
    public RoomRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<ResponseId> CreateWithImage(RoomDto dto, Stream? image = null)
    {
        var entity = (HosRoom)dto;

        try
        {
            if (image != null)
            {
                var imageName = Helper.GenerateImageName();
                _ = DataAccessImageService.SaveSingleImage(image, imageName);
                entity.Photo = imageName;
            }
            else
                entity.Photo = null;

            var result = await Context.HosRooms.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
                entity.CodeNumber = "bui-" + Context.HosRooms.Count().ToString();
            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new ResponseId(true, "created ", result.Entity.Id);
            }
            return new ResponseId(false, "No row effected ", 0);
        }
        catch (Exception ex)
        {
            return new ResponseId(false, ex.Message + ex.InnerException?.Message, 0);
        }
    }
    #endregion


    #region Update
    public async Task<Response<RoomDto?>> Update(RoomDto dto, int id, Stream? image = null)
    {
        Response<RoomDto?> respons;
        try
        {
            var current = Context.HosRooms.Find(id);
            if (current == null)
                return respons = new(false, $"id: {id} is not found");
            dto.Id = id;
            var imageName = "";
            var modfied = false;

            if (image != null)
            {
                // if photo in database is null
                if (string.IsNullOrEmpty(current.Photo))
                {
                    imageName = Helper.GenerateImageName();
                    _ = DataAccessImageService.SaveSingleImage(image, imageName);
                    dto.Photo = imageName;
                    modfied = true;
                }
                else
                {
                    _ = DataAccessImageService.UpdateSingleImage(image, current.Photo);
                    modfied = false;
                }
            }

            current = dto;
            Context.Update(current).Property(propa => propa.Photo).IsModified = modfied;
            Context.Entry(current).Property(p => p.IsDeleted).IsModified = false;
            //Context.Entry(current).Property(p => p.CreateOn).IsModified = false;
            await Context.SaveChangesAsync();
            return respons = new(true, $"update on id: {id}", null);
        }
        catch (Exception ex)
        {
            return respons = new(false, "can not duplicate foreignKey with same Id ......." + ex.InnerException?.Message + ex.Message, null); ;
        }
    }
    #endregion


    #region Read
    public async Task<RoomDto?> ReadById(int? Id, string? lang = null)
    {
        try
        {
            var query = Context.HosRooms.Where(x => x.Id == Id);
            return await query
                .Include(type => type.RoomType)
                .ThenInclude(n => n.RoomTypeTranslations)
                .Select(room => new RoomDto
                {
                    Id = room.Id,
                    CodeNumber = room.CodeNumber,
                    Photo = room.Photo,
                    IsDeleted = room.IsDeleted,
                    RoomTypeId = room.RoomTypeId,
                    FloorId = room.FloorId,
                    BuildId = room.BuildId,
                    HospitalId = room.HospitalId,
                    Kind = room.RoomType.RoomTypeTranslations.Where(la => la.LangCode == lang).FirstOrDefault()!.Name,
                    RoomTranslations = lang == null ? room.RoomTranslations.ToList() : room.RoomTranslations.Where(t => t.LangCode == lang).ToList(),
                })
                .FirstOrDefaultAsync();
            //return await ss;
        }
        catch (Exception)
        {
            return null;
        }
    }


    public async Task<PagedResponse<RoomDto>?> ReadAll(int? roomTypeId, int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page)
    {
        IQueryable<HosRoom> query = Context.HosRooms;

        var total = 0;
        if (status is not null)
        {

            if (status.Equals("inactive"))
            {
                query = query.Where(h => h.IsDeleted);
            }
            else if (status.Equals("active"))
            {
                query = query.Where(h => !h.IsDeleted);
            }
        }

        if (isBaseActive is not null)
        {
            if (isBaseActive.Value)
                query = query.Where(h => !h.Floor.IsDeleted);
            else
                query = query.Where(h => h.Floor.IsDeleted);
        }

        if (baseid.HasValue)
        {
            query = query.Where(ho => ho.FloorId.Equals(baseid));
        }
        if (roomTypeId is not null)
        {
            query = query.Where(ty => ty.RoomTypeId == roomTypeId);
        }

        query = query.OrderByDescending(o => o.Id);

        total = query.Count();
        if (total < 0)
            return null;


        // page size
        GenericPagination(ref query, ref pageSize, ref page, total);


        // lang
        if (lang is not null)
        {
            query = query.Include(tranc1 => tranc1.RoomTranslations.Where(t => t.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.RoomTranslations);
        }
        var result = await query
             .Select(room => new RoomDto
             {
                 Id = room.Id,
                 CodeNumber = room.CodeNumber,
                 Photo = room.Photo,
                 IsDeleted = room.IsDeleted,
                 RoomTypeId = room.RoomTypeId,
                 FloorId = room.FloorId,
                 BuildId = room.BuildId,
                 HospitalId = room.HospitalId,
                 Kind = room.RoomType.RoomTypeTranslations.Where(la => la.LangCode == lang).FirstOrDefault()!.Name,
                 RoomTranslations = lang == null ? room.RoomTranslations.ToList() : room.RoomTranslations.Where(t => t.LangCode == lang).ToList(),
             })
             .ToListAsync();

        PagedResponse<RoomDto> all = new()
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Data = result
        };
        return all;
    }



    public async Task<PagedResponse<RoomDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize)
    {
        var query = from h in Context.HosRooms
                    join t in Context.RoomTranslations on h.Id equals t.RoomId
                    where (h.CodeNumber.Contains(searchTerm) || t.Name.Contains(searchTerm))
                          && t.LangCode == lang
                    select new RoomDto
                    {
                        Id = h.Id,
                        Photo = h.Photo,
                        CodeNumber = h.CodeNumber,
                        IsDeleted = h.IsDeleted,
                        HospitalId = h.HospitalId,
                        BuildId = h.BuildId,
                        FloorId = h.FloorId,
                        Kind = h.Kind,
                        RoomTypeId = h.RoomTypeId,
                        RoomTranslations = new List<RoomTranslation> { t }
                    };

        if (isActive.HasValue)
        {
            if (!isActive.Value)
            {
                query = query.Where(h => h.IsDeleted);
            }
            else if (isActive.Value)
            {
                query = query.Where(h => !h.IsDeleted);
            }
        }

        var totalCount = await query.CountAsync();

        GenericPagination(ref query, ref pageSize, ref page, totalCount);

        var listDto = await query.OrderByDescending(h => h.Id)
                                 .ToListAsync();

        var all = new PagedResponse<RoomDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = listDto
        };
        return all;
    }

    // old
    //public async Task<PagedResponse<RoomDto>?> SearchByNameOrCode1(string searchTerm, string lang = "ar", int page = 1, int pageSize = Constants.PageSize)
    //{
    //    int skip = Helper.SkipValue(page, pageSize);
    //    IQueryable<HosRoom> query = Context.HosRooms;

    //    var hospitals = await Context.HosRooms
    //       .Join(Context.RoomTranslations,
    //           h => h.Id,
    //           t => t.RoomId,
    //           (h, t) => new { HosRoom = h, Translation = t })
    //       .Where(x => (x.HosRoom.CodeNumber.Contains(searchTerm) && x.Translation.LangCode == lang) || x.Translation.Name.Contains(searchTerm) && x.Translation.LangCode == lang)
    //       .Skip(skip).Take(pageSize)
    //       .Select(x => new RoomDto
    //       {
    //           Id = x.HosRoom.Id,
    //           Photo = x.HosRoom.Photo,
    //           CodeNumber = x.HosRoom.CodeNumber,
    //           RoomTranslations = new List<RoomTranslation> { x.Translation }
    //       })
    //       .ToListAsync();

    //    var all = new PagedResponse<RoomDto>
    //    {
    //        Total = hospitals.Count,
    //        Page = page,
    //        PageSize = pageSize,
    //        Data = hospitals
    //    };
    //    return all;
    //}

    #endregion
}
