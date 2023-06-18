using DataAccess.Contexts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Entities;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models.Floors;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using DomainModel.Models.MedicalSpecialteis;

namespace DataAccess.Repositories;

public class MedicalSpecialtyRepository : GenericRepository, IMedicalSpecialtyRepository
{
    public MedicalSpecialtyRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<ResponseId> CreateWithImage(MedicalSpecialtyDto dto, Stream? image = null)
    {
        var entity = (MedicalSpecialty)dto;

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

            var result = await Context.MedicalSpecialties.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
                entity.CodeNumber = "bui-" + Context.MedicalSpecialties.Count().ToString();
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
    public async Task<Response<MedicalSpecialtyDto?>> Update(MedicalSpecialtyDto dto, int id, Stream? image = null)
    {
        Response<MedicalSpecialtyDto?> respons;
        try
        {
            var current = Context.MedicalSpecialties.Find(id);
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
            //Context.Entry(current).Property(p => p.Appearance).IsModified = false;
            await Context.SaveChangesAsync();
            return respons = new(true, $"update on id: {id}", null);
        }
        catch (Exception ex)
        {
            return respons = new(false, "can not duplicate foreignKey with same Id ......." + ex.Message+"________and _______"+ex.InnerException?.Message, null); ;
        }
    }
    #endregion


    #region Read
    public async Task<MedicalSpecialtyDto?> ReadById(int? Id, string? lang = null)
    {
        var _context = Context.MedicalSpecialties.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.MedicalSpecialtyTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.MedicalSpecialtyTranslations.Where(la => la.LangCode == lang));
        }
        try
        {
            var entity = await _context
                .SingleOrDefaultAsync();
            return entity!;
        }
        catch (Exception)
        {
            return null;
        }
    }


    public async Task<PagedResponse<MedicalSpecialtyDto>?> ReadAll(int? baseid, bool? appearance, string? status, string? lang, int? pageSize, int? page)
    {

        IQueryable<MedicalSpecialty> query = Context.MedicalSpecialties;

        var totalCount = 0;
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

        if (appearance is not null)
        {

            if (appearance.Value)
                query = query.Where(h => h.Appearance);
            else
                query = query.Where(h => !h.Appearance);
        }

        if (baseid.HasValue)
        {
            query = query.Where(s => s.Hospitals.Any(h => h.Id == baseid));
        }

        query = query.OrderByDescending(o => o.Id);

        totalCount = query.Count();
        if (totalCount < 0)
            return null;

        // page size
        GenericPagination(ref query, ref pageSize, ref page, totalCount);

        // lang
        if (lang is not null)
        {
            query = query.Include(tranc1 => tranc1.MedicalSpecialtyTranslations.Where(post => post.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.MedicalSpecialtyTranslations);
        }

        await query.ToListAsync();

        var result = MedicalSpecialtyDto.ToList(query);
        var all = new PagedResponse<MedicalSpecialtyDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = result.ToList()
        };

        return all;
    }


    public async Task<PagedResponse<MedicalSpecialtyDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize)
    {
        var query = from h in Context.MedicalSpecialties
                    join t in Context.MedicalSpecialtyTranslations on h.Id equals t.MedicalSpecialtyId
                    where (h.CodeNumber.Contains(searchTerm) || t.Name.Contains(searchTerm))
                          && t.LangCode == lang
                    select new MedicalSpecialtyDto
                    {
                        Id = h.Id,
                        Photo = h.Photo,
                        CodeNumber = h.CodeNumber,
                        Appearance = h.Appearance,
                        //IsActive = h.IsActive,
                        IsDeleted = h.IsDeleted,
                        MedicalSpecialtyTranslations = new List<MedicalSpecialtyTranslation> { t }
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

        var all = new PagedResponse<MedicalSpecialtyDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = listDto
        };

        return all;
    }
    #endregion


    /* deleted
         public async Task<AllMedicalSpecialtyDto?> SearchByNameOrCode1(string searchTerm, string lang, int page, int pageSize)
    {
        int skip = Helper.SkipValue(page, pageSize);
        IQueryable<MedicalSpecialty> query = Context.MedicalSpecialties;

        var totalCount = await query
            .Join(Context.MedicalSpecialtyTranslations,
                h => h.Id,
                t => t.MedicalSpecialtyId,
                (h, t) => new { MedicalSpecialty = h, Translation = t })
            .Where(x => (x.MedicalSpecialty.CodeNumber.Contains(searchTerm) && x.Translation.LangCode == lang) ||
                        x.Translation.Name.Contains(searchTerm) && x.Translation.LangCode == lang)
            .CountAsync();

        var hospitals = await query
            .Join(Context.MedicalSpecialtyTranslations,
                h => h.Id,
                t => t.MedicalSpecialtyId,
                (h, t) => new { MedicalSpecialty = h, Translation = t })
            .Where(x => (x.MedicalSpecialty.CodeNumber.Contains(searchTerm) && x.Translation.LangCode == lang) ||
                        x.Translation.Name.Contains(searchTerm) && x.Translation.LangCode == lang)
            .Select(x => new MedicalSpecialtyDto
            {
                Id = x.MedicalSpecialty.Id,
                Photo = x.MedicalSpecialty.Photo,
                CodeNumber = x.MedicalSpecialty.CodeNumber,
                Appearance = x.MedicalSpecialty.Appearance,
                IsActive = x.MedicalSpecialty.IsActive,
                IsDeleted = x.MedicalSpecialty.IsDeleted,
                MedicalSpecialtyTranslations = new List<MedicalSpecialtyTranslation> { x.Translation }
            })
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var all = new AllMedicalSpecialtyDto
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            MedicalSpecialties = hospitals
        };

        return all;
    }
     
     
     */
}
