using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Rooms;
using Microsoft.EntityFrameworkCore;

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
            return new ResponseId(false, ex.Message, 0);
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
        var _context = Context.HosRooms.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.RoomTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.RoomTranslations.Where(la => la.LangCode == lang));
        }
        try
        {
            var entity = await _context
                .SingleOrDefaultAsync();
            return entity!;
        }
        catch (ArgumentNullException)
        {
            return null;
        }
    }

    public async Task<AllRoomDto?> ReadAll(bool? isBuildActive, string isActive, string? lang, int page = 1, int pageSize = Constants.PageSize)
    {
        int skip = Helper.SkipValue(page, pageSize);
        IQueryable<HosRoom> query = Context.HosRooms;

        var total = 0;

        if (isActive == "inactive")
        {
            query = query.Where(h => h.IsDeleted == true);
            total = Context.HosRooms.Count(h => h.IsDeleted == true);
        }
        else if (isActive == "active")
        {
            query = query.Where(h => h.IsDeleted == false);
            total = Context.HosRooms.Count(h => h.IsDeleted == false);
        }


        if (isBuildActive != null)
        {
            if (isBuildActive.Value)
                query = query.Where(h => h.Build.IsDeleted == false);
            else
                query = query.Where(h => h.Build.IsDeleted == true);
        }



        if (total < 1) return null;
        query = query.Skip(skip).Take(pageSize);

        if (lang != null)
        {
            query = query.Include(tranc1 => tranc1.RoomTranslations
                         .Where(post => post.LangCode == lang)
                         .OrderBy(post => post.Name));
        }
        else
        {
            query = query
                .Include(tranc2 => tranc2.RoomTranslations);
        }
        query = query.OrderByDescending(o => o.Id);
        await query.ToListAsync();

        var all = new AllRoomDto();
        var result = RoomDto.ToList(query);
        all.Total = total;
        all.Page = page;
        all.PageSize = pageSize;
        all.Rooms = result.ToList();
        return all;
    }

    public async Task<List<RoomTranslation>> SearchByName(string name)
    {
        IQueryable<RoomTranslation> query = Context.RoomTranslations;

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(t => t.Name.Contains(name) && t.Room!= null && t.Room.IsDeleted == false);
        }

        List<RoomTranslation> results = await query.ToListAsync();
        return results;
    }

    public async Task<AllRoomDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = Constants.PageSize)
    {
        int skip = Helper.SkipValue(page, pageSize);
        IQueryable<HosRoom> query = Context.HosRooms;

        var hospitals = await Context.HosRooms
           .Join(Context.RoomTranslations,
               h => h.Id,
               t => t.RoomId,
               (h, t) => new { HosRoom = h, Translation = t })
           .Where(x => (x.HosRoom.CodeNumber.Contains(searchTerm) && x.Translation.LangCode == lang) || x.Translation.Name.Contains(searchTerm) && x.Translation.LangCode == lang)
           .Skip(skip).Take(pageSize)
           .Select(x => new RoomDto
           {
               Id = x.HosRoom.Id,
               Photo = x.HosRoom.Photo,
               CodeNumber = x.HosRoom.CodeNumber,
               RoomTranslations = new List<RoomTranslation> { x.Translation }
           })
           .ToListAsync();

        var all = new AllRoomDto
        {
            Total = hospitals.Count,
            Page = page,
            PageSize = pageSize,
            Rooms = hospitals
        };
        return all;
    }
    #endregion
}
