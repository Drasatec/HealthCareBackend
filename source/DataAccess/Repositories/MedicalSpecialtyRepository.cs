using DataAccess.Contexts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Entities;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models.Floors;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Context.Entry(current).Property(p => p.Appearance).IsModified = false;
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

    public async Task<AllMedicalSpecialtyDto?> ReadAll(int? baseid, bool? isBaseActive, string? status, string? lang, int? pageSize, int page = 1)
    {
        IQueryable<MedicalSpecialty> query = Context.MedicalSpecialties;

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

        //if (isBaseActive is not null)
        //{
        //    if (isBaseActive.Value)
        //        query = query.Where(h => !h.Build.IsDeleted);
        //    else
        //        query = query.Where(h => h.Build.IsDeleted);
        //}

        if (baseid.HasValue)
        {
            query = query.Where(ho => ho.Hospitals.Where(x=>x.Id.Equals(baseid)).SingleOrDefault().Id == baseid);
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
            query = query.Include(tranc1 => tranc1.MedicalSpecialtyTranslations.Where(post => post.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.MedicalSpecialtyTranslations);
        }

        await query.ToListAsync();

        var all = new AllMedicalSpecialtyDto();
        var result = MedicalSpecialtyDto.ToList(query);
        all.Total = total;
        all.Page = page;
        all.PageSize = pageSize!.Value;
        all.MedicalSpecialties = result.ToList();
        return all;
    }


    public async Task<List<MedicalSpecialtyTranslation>> SearchByName(string name, int? buildId)
    {
        IQueryable<MedicalSpecialtyTranslation> query = Context.MedicalSpecialtyTranslations;

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(t => t.Name.Contains(name));
        }

        //if (buildId.HasValue)
        //{
        //    query = query.Where(ho => ho.Floor != null && ho.Floor.BuildId.Equals(buildId));
        //}

        return await query.ToListAsync();
    }

    public async Task<AllMedicalSpecialtyDto?> SearchByNameOrCode(string searchTerm, string lang = "ar", int page = 1, int pageSize = Constants.PageSize)
    {
        int skip = Helper.SkipValue(page, pageSize);
        IQueryable<MedicalSpecialty> query = Context.MedicalSpecialties;

        var hospitals = await Context.MedicalSpecialties
           .Join(Context.MedicalSpecialtyTranslations,
               h => h.Id,
               t => t.MedicalSpecialtyId,
               (h, t) => new { MedicalSpecialty = h, Translation = t })
           .Where(x => (x.MedicalSpecialty.CodeNumber.Contains(searchTerm) && x.Translation.LangCode == lang) || x.Translation.Name.Contains(searchTerm) && x.Translation.LangCode == lang)
           .Skip(skip).Take(pageSize)
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
           .ToListAsync();

        var all = new AllMedicalSpecialtyDto
        {
            Total = hospitals.Count,
            Page = page,
            PageSize = pageSize,
            MedicalSpecialties = hospitals
        };
        return all;
    }

    #endregion
}
