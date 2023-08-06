using DataAccess.Contexts;
using DomainModel.Entities.HospitalBody;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Buildings;
using DomainModel.Models.Floors;
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

    public async Task<PagedResponse<BuildingDto>?> ReadAll(int? hosId, bool? isHosActive, string? status, string? lang, int? pageSize, int? page)
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
        GenericPagination(ref query, ref pageSize, ref page, total);

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

        var all = new PagedResponse<BuildingDto>();
        var result = BuildingDto.ToList(query);
        all.Total = total;
        all.Page = page;
        all.PageSize = pageSize;
        all.Data = result.ToList();
        return all;
    }



    public async Task<PagedResponse<BuildingDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize)
    {
        var query = from h in Context.HosBuildings
                    join t in Context.BuildingTranslations on h.Id equals t.BuildeingId
                    where (h.CodeNumber.Contains(searchTerm) || t.Name.Contains(searchTerm))
                          && t.LangCode == lang
                    select new BuildingDto
                    {
                        Id = h.Id,
                        Photo = h.Photo,
                        CodeNumber = h.CodeNumber,
                        IsDeleted = h.IsDeleted,
                        HospitalId = h.HospitalId,
                        BuildingTranslation = new List<BuildingTranslation> { t }
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

        var all = new PagedResponse<BuildingDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = listDto
        };
        return all;
    }
    #endregion
}
