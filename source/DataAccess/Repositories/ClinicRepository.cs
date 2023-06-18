using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class ClinicRepository : GenericRepository, IClinicRepository
{
    public ClinicRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<ResponseId> CreateWithImage(ClinicDto dto, Stream? image = null)
    {
        var entity = (Clinic)dto;

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

            var result = await Context.Clinics.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
                entity.CodeNumber = "clinic-" + Context.Clinics.Count().ToString();
            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new ResponseId(true, "created ", result.Entity.Id);
            }
            return new ResponseId(false, "No row effected ", 0);
        }
        catch (Exception ex)
        {
            return new ResponseId(false, ex.Message+"____and____"+ ex.InnerException?.Message, 0);
        }
    }
    #endregion


    #region Update
    public async Task<Response<ClinicDto?>> Update(ClinicDto dto, int id, Stream? image = null)
    {
        Response<ClinicDto?> respons;
        try
        {
            var current = Context.Clinics.Find(id);
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
            return respons = new(false, "can not duplicate foreignKey with same Id ......." + ex.Message, null); ;
        }
    }
    #endregion


    #region Read
    public async Task<ClinicDto?> ReadById(int? Id, string? lang = null)
    {
        var _context = Context.Clinics.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.ClinicTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.ClinicTranslations.Where(la => la.LangCode == lang));
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


    public async Task<PagedResponse<ClinicDto>?> ReadAll(int? baseid, bool? appearance, string? status, string? lang, int? pageSize, int? page)
    {

        IQueryable<Clinic> query = Context.Clinics;

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
            query = query.Where(s => s.SpecialtyId == baseid);
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
            query = query.Include(tranc1 => tranc1.ClinicTranslations.Where(post => post.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.ClinicTranslations);
        }

        await query.ToListAsync();

        var result = ClinicDto.ToList(query);
        var all = new PagedResponse<ClinicDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = result.ToList()
        };

        return all;
    }


    public async Task<PagedResponse<ClinicDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize)
    {
        var query = from h in Context.Clinics
                    join t in Context.ClinicTranslations on h.Id equals t.ClinicId
                    where (h.CodeNumber.Contains(searchTerm) || t.Name.Contains(searchTerm))
                          && t.LangCode == lang
                    select new ClinicDto
                    {
                        Id = h.Id,
                        Photo = h.Photo,
                        CodeNumber = h.CodeNumber,
                        Appearance = h.Appearance,
                        //IsActive = h.IsActive,
                        IsDeleted = h.IsDeleted,
                        ClinicTranslations = new List<ClinicTranslation> { t }
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

        var all = new PagedResponse<ClinicDto>
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
