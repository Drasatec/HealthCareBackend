using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Buildings;
using DomainModel.Models.Floors;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class FloorRepository : GenericRepository, IFloorRepository
{
    public FloorRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<ResponseId> CreateWithImage(FloorDto dto, Stream? image = null)
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

    public async Task<AllFloorDto?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int page = 1)
    {
        IQueryable<HosFloor> query = Context.HosFloors;

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
                query = query.Where(h => !h.Build.IsDeleted);
            else
                query = query.Where(h => h.Build.IsDeleted);
        }

        if (baseid.HasValue)
        {
            query = query.Where(ho => ho.BuildId.Equals(baseid));
        }

        query = query.OrderByDescending(o => o.Id);

        total = query.Count();
        if (total < 0)
            return null;


        // page size
        if (pageSize.HasValue)
        {
            GenericPagination(ref query, pageSize.Value, page);
        }
        else pageSize = total;

        // lang
        if (lang is not null)
        {
            query = query.Include(tranc1 => tranc1.FloorTranslations.Where(post => post.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.FloorTranslations);
        }

        await query.ToListAsync();

        var all = new AllFloorDto();
        var result = FloorDto.ToList(query);
        all.Total = total;
        all.Page = page;
        all.PageSize = pageSize!.Value;
        all.Floors = result.ToList();
        return all;
    }


    public async Task<List<FloorTranslation>> SearchByName(string name, int? buildId)
    {
        IQueryable<FloorTranslation> query = Context.FloorTranslations;

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(t => t.Name.Contains(name));
        }

        if (buildId.HasValue)
        {
            query = query.Where(ho => ho.Floor != null && ho.Floor.BuildId.Equals(buildId));
        }

        return  await query.ToListAsync();
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
