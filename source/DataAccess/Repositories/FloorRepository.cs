using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Floors;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class FloorRepository : GenericRepository, IFloorRepository
{
    public FloorRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<Response> CreateWithImage(FloorDto dto, Stream? image = null)
    {
        var entity = (HosFloor)dto;

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

            var result = await Context.HosFloors.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
                entity.CodeNumber = "bui-" + Context.HosFloors.Count().ToString();
            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new Response(true, "id: " + result.Entity.Id);
            }
            return new Response(false, "No row effected "); //(FloorDto) result.Entity;
        }
        catch (Exception ex)
        {
            return new Response(false, ex.Message);
        }
    }
    #endregion


    #region Update
    public async Task<Response<FloorDto?>> Update(FloorDto dto, int id, Stream? image = null)
    {
        Response<FloorDto?> respons;
        try
        {
            var current = Context.HosFloors.Find(id);
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
            Context.Entry(current).Property(p => p.CreateOn).IsModified = false;
            await Context.SaveChangesAsync();
            return respons = new(true, $"update on id: {id}", null);
        }
        catch (Exception ex)
        {
            return respons = new(false, "can not duplicate foreignKey with same Id ......." + ex.Message, null); ;
        }
    }
    #endregion


    #region Read
    public async Task<FloorDto?> ReadById(int? Id, string? lang = null)
    {
        var _context = Context.HosFloors.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.FloorTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.FloorTranslations.Where(la => la.LangCode == lang));
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

    public async Task<AllFloorDto?> ReadAll(bool? isBuildActive, string isActive, string? lang, int page = 1, int pageSize = Constants.PageSize)
    {
        int skip = Helper.SkipValue(page, pageSize);
        IQueryable<HosFloor> query = Context.HosFloors;

        var total = 0;

        if (isActive == "inactive")
        {
            query = query.Where(h => h.IsDeleted == true);
            total = Context.HosFloors.Count(h => h.IsDeleted == true);
        }
        else if (isActive == "active")
        {
            query = query.Where(h => h.IsDeleted == false);
            total = Context.HosFloors.Count(h => h.IsDeleted == false);
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
            query = query.Include(tranc1 => tranc1.FloorTranslations
                         .Where(post => post.LangCode == lang)
                         .OrderBy(post => post.Name));
        }
        else
        {
            query = query
                .Include(tranc2 => tranc2.FloorTranslations);
        }
        query = query.OrderByDescending(o => o.CreateOn);
        await query.ToListAsync();

        var all = new AllFloorDto();
        var result = FloorDto.ToList(query);
        all.Total = total;
        all.Page = page;
        all.PageSize = pageSize;
        all.Floors = result.ToList();
        return all;
    }

    public async Task<List<FloorTranslation>> SearchByName(string name)
    {
        IQueryable<FloorTranslation> query = Context.FloorTranslations;

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(t => t.Name.Contains(name) && t.Floor.IsDeleted == false);
        }

        List<FloorTranslation> results = await query.ToListAsync();
        return results;
    }

    public async Task<AllFloorDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = Constants.PageSize)
    {
        int skip = Helper.SkipValue(page, pageSize);
        IQueryable<HosFloor> query = Context.HosFloors;

        var hospitals = await Context.HosFloors
           .Join(Context.FloorTranslations,
               h => h.Id,
               t => t.FloorId,
               (h, t) => new { HosFloor = h, Translation = t })
           .Where(x => (x.HosFloor.CodeNumber.Contains(searchTerm) && x.Translation.LangCode == lang) || x.Translation.Name.Contains(searchTerm) && x.Translation.LangCode == lang)
           .Skip(skip).Take(pageSize)
           .Select(x => new FloorDto
           {
               Id = x.HosFloor.Id,
               Photo = x.HosFloor.Photo,
               CodeNumber = x.HosFloor.CodeNumber,
               FloorTranslations = new List<FloorTranslation> { x.Translation }
           })
           .ToListAsync();

        var all = new AllFloorDto
        {
            Total = hospitals.Count,
            Page = page,
            PageSize = pageSize,
            Floors = hospitals
        };
        return all;
    }
    #endregion
}
