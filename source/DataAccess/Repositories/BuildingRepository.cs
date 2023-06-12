using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Buildings;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Repositories;

public class BuildingRepository : GenericRepository, IBuildingRepository
{
    public BuildingRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<ResponseId> CreateWithImage(BuildingDto dto, Stream? image = null)
    {
        var entity = (HosBuilding)dto;

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

            var result = await Context.HosBuildings.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
                entity.CodeNumber = "bui-" + Context.HosBuildings.Count().ToString();
            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new ResponseId(true, "created", result.Entity.Id);
            }
            return new ResponseId(false, "No row effected ", result.Entity.Id); //(BuildingDto) result.Entity;
        }
        catch (Exception ex)
        {
            return new ResponseId(false, ex.Message, 0);
        }


    }
    #endregion


    #region Update
    public async Task<Response<BuildingDto?>> Update(BuildingDto dto, int id, Stream? image = null)
    {
        Response<BuildingDto?> respons;
        try
        {
            var current = Context.HosBuildings.Find(id);
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
    public async Task<BuildingDto?> ReadById(int? Id, string? lang = null)
    {
        var _context = Context.HosBuildings.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.BuildingTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.BuildingTranslations.Where(la => la.LangCode == lang));
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

    public async Task<AllBuildingDto?> ReadAll(int? hosId, bool? isHosActive, string? status, string? lang, int? pageSize, int page = 1)
    {
        IQueryable<HosBuilding> query = Context.HosBuildings;

        var total = 0;
        if (status is not null)
        {

            if (status.Equals("inactive"))
            {
                query = query.Where(h => h.IsDeleted);
                //total = Context.HosBuildings.Count(h => h.IsDeleted);
            }
            else if (status.Equals("active"))
            {
                query = query.Where(h => !h.IsDeleted);
                //total = Context.HosBuildings.Count(h => !h.IsDeleted);
            }
        }
        //else
        //    total = Context.HosBuildings.Count();

        //if (total < 1) 
        //    return null;

        if (isHosActive is not null)
        {
            if (isHosActive.Value)
                query = query.Where(h => !h.Hospital.IsDeleted);
            else
                query = query.Where(h => h.Hospital.IsDeleted);
        }

        if (hosId.HasValue)
        {
            query = query.Where(ho => ho.HospitalId.Equals(hosId));
        }

        query = query.OrderByDescending(o => o.Id);

        total = query.Count();
        if (total < 0)
            return null;


        // page size
        if (pageSize.HasValue)
        {
            int skip = Helper.SkipValue(page, pageSize.Value);
            query = query.Skip(skip).Take(pageSize.Value);
        }
        else pageSize = total;

        // lang
        if (lang is not null)
        {
            query = query.Include(tranc1 => tranc1.BuildingTranslations.Where(post => post.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.BuildingTranslations);
        }

        await query.ToListAsync();

        var all = new AllBuildingDto();
        var result = BuildingDto.ToList(query);
        all.Total = total;
        all.Page = page;
        all.PageSize = pageSize!.Value;
        all.Buildings = result.ToList();
        return all;
    }

    public async Task<List<BuildingTranslation>> SearchByName(string name, int? hosId)
    {
        IQueryable<BuildingTranslation> query = Context.BuildingTranslations;

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(t => t.Name.Contains(name));
            //query = query.Where(t => t.Name.Contains(name)&& t.Buildeing!= null && t.Buildeing.IsDeleted == false);
        }

        if (hosId.HasValue)
        {
            query = query.Where(ho => ho.Buildeing != null && ho.Buildeing.HospitalId.Equals(hosId));
        }

        return await query.ToListAsync();
    }

    public async Task<AllBuildingDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = Constants.PageSize)
    {
        int skip = Helper.SkipValue(page, pageSize);

        var query = await Context.HosBuildings
           .Join(Context.BuildingTranslations,
               h => h.Id,
               t => t.BuildeingId,
               (h, t) => new { HosBuilding = h, Translation = t })

           .Where(x => (x.HosBuilding.CodeNumber.Contains(searchTerm) && x.Translation.LangCode == lang) || x.Translation.Name.Contains(searchTerm) && x.Translation.LangCode == lang)
           .Skip(skip).Take(pageSize)
           .Select(x => new BuildingDto
           {
               Id = x.HosBuilding.Id,
               Photo = x.HosBuilding.Photo,
               CodeNumber = x.HosBuilding.CodeNumber,
               HospitalId = x.HosBuilding.HospitalId,
               BuildingTranslation = new List<BuildingTranslation> { x.Translation }
           })
           .ToListAsync();

        var all = new AllBuildingDto
        {
            Total = query.Count,
            Page = page,
            PageSize = pageSize,
            Buildings = query
        };
        return all;
    }
    #endregion
}
