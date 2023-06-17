using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Buildings;
using DomainModel.Models.Floors;
using DomainModel.Models.Rooms;
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

    public async Task<AllFloorDto?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int? page)
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
        GenericPagination(ref query, ref pageSize, ref page, total);

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
        all.PageSize = pageSize;
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

    public async Task<AllFloorDto?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize)
    {
        var query = from h in Context.HosFloors 
                    join t in Context.FloorTranslations on h.Id equals t.FloorId
                    where (h.CodeNumber.Contains(searchTerm) || t.Name.Contains(searchTerm))
                          && t.LangCode == lang
                    select new FloorDto
                    {
                        Id = h.Id,
                        Photo = h.Photo,
                        CodeNumber = h.CodeNumber,
                        IsDeleted = h.IsDeleted,
                        HospitalId = h.HospitalId,
                        BuildId = h.BuildId,
                        CreateOn = h.CreateOn,
                        FloorTranslations= new List<FloorTranslation> { t }
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

        var listDto = await query.OrderByDescending(h => h.Id).ToListAsync();

        var all = new AllFloorDto
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Floors = listDto
        };
        return all;
    }

    #endregion
}
