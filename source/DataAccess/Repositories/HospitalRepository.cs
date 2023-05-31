﻿using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Hospitals;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Repositories;
public class HospitalRepository : GenericRepository<HospitalDto>, IHospitalRepository
{
    public HospitalRepository(AppDbContext context) : base(context) { }

    #region Add
    public async Task<HospitalDto?> CreateWithImage(HospitalDto dto, Stream? image = null)
    {
        var entity = (Hospital)dto;

        if (image != null)
        {
            var imageName = Helper.GenerateImageName();
            _ = DataAccessImageService.SaveSingleImage(image, imageName);
            entity.Photo = imageName;
        }

        var result = await Context.Hospitals.AddAsync(entity);

        if (entity.HospitalPhoneNumbers != null)
        {
            if (entity.HospitalPhoneNumbers.Count < 1)
            {
                entity.HospitalPhoneNumbers.Add(new() { Id = result.Entity.Id, TelephoneNumber = "0" });
            }
        }

        await Context.SaveChangesAsync();

        return await ReadHospitalById(result.Entity.Id); //(HospitalDto) result.Entity;
    }

    public async Task<Response<HospitalDto?>> AddTranslations(List<HospitalTranslation> dto, int id)
    {
        var res = new Response<HospitalDto?>();
        try
        {
            foreach (var translation in dto)
            {
                translation.HospitalId = id;
            }
            await Context.HospitalTranslations.AddRangeAsync(dto);
            await Context.SaveChangesAsync();
            res.Value = await ReadHospitalById(id);
            res.Success = true;
            return res;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.Message = "can not duplicate langCode with same hosId ......." + ex.Message;
            return res;
        }
    }

    public async Task<Response<HospitalDto?>> AddPhoneNumbers(List<HospitalPhoneNumber> dto, int id)
    {
        var res = new Response<HospitalDto?>();
        try
        {
            foreach (var translation in dto)
            {
                translation.HospitalId = id;
            }
            await Context.HospitalPhoneNumbers.AddRangeAsync(dto);
            await Context.SaveChangesAsync();
            res.Value = await ReadHospitalById(id);
            res.Success = true;
            return res;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.Message = ex.Message;
            return res;
        }
    }
    #endregion

    #region Update

    public async Task<Response<HospitalDto?>> UpdateHospital(HospitalDto dto, int id, Stream? image = null)
    {
        Response<HospitalDto?> respons;
        try
        {
            var current = Context.Hospitals.Find(id);
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

            return respons = new(true, null, await ReadHospitalById(id));
        }
        catch (Exception ex)
        {
            return respons = new(false, "can not duplicate langCode with the same hosId or ....."+ ex.Message, null); ;
        }
    }

    public async Task<Response<string?>> UpdateAnImage(Stream image, int id)
    {
        Response<string?> res;
        var current = Context.Hospitals.Find(id);
        if (current == null)
        {
            res = new(false, "id is not found", null);
            return res;
        }

        var imageName = current.Photo;

        if (string.IsNullOrEmpty(current.Photo))
        {
            imageName = Helper.GenerateImageName();
            _ = DataAccessImageService.SaveSingleImage(image, imageName);
            current.Photo = imageName;

            Context.Update(current).Property(p => p.CodeNumber).IsModified = false;
            Context.Entry(current).Property(p => p.IsDeleted).IsModified = false;
            Context.Entry(current).Property(p => p.CreateOn).IsModified = false;
            Context.Entry(current).Property(p => p.WhatsAppNumber).IsModified = false;
            Context.Entry(current).Property(p => p.Email).IsModified = false;
            

            await Context.SaveChangesAsync();
        }

        else
        {
            _ = DataAccessImageService.UpdateSingleImage(image, current.Photo);
        }

        return res = new(true, "", imageName);
    }

    public async Task<Response> DeleteHospitalById(int id, bool isDeleted)
    {
        try
        {
            var current = await Context.Hospitals.Where(hos => hos.Id == id).SingleOrDefaultAsync();
            if (current != null)
            {
                current.IsDeleted = isDeleted;
                Context.Update(current);
                Context.Entry(current).Property(p => p.Photo).IsModified = false;
                Context.Entry(current).Property(p => p.CodeNumber).IsModified = false;
                var rowEffict = await Context.SaveChangesAsync();
                if (rowEffict > 0) return new Response(true, $"updte on hosId: {id} ");
            }
            return new Response(false, $"Id : {id} is not found  "); ;
        }
        catch (Exception)
        {
            return new Response(false, $"No changes on hosId: {id} ");
        }
    }

    #endregion

    #region Read

    public async Task<HospitalDto?> ReadHospitalById(int Id, string? lang = null)
    {
        var _context = Context.Hospitals.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.HospitalTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.HospitalTranslations.Where(la => la.LangCode == lang));
        }
        try
        {
            var entity = await _context
                .Include(contact => contact.HospitalPhoneNumbers)
                .SingleOrDefaultAsync();
            return entity!;
        }
        catch (ArgumentNullException)
        {
            return null;
        }
    }


    public async Task<AllHospitalsDto?> ReadAllHospitals(string isActive, string? lang, int page = 1, int pageSize = 10)
    {
        int skip = Helper.SkipValue(page, pageSize);
        IQueryable<Hospital> query = Context.Hospitals;

        var total = 0;

        if (isActive == "inactive")
        {
            query = query.Where(h => h.IsDeleted == true);
            total = Context.Hospitals.Count(h => h.IsDeleted == true);
        }

        else if (isActive == "active")
        {
            query = query.Where(h => h.IsDeleted == false);
            total = Context.Hospitals.Count(h => h.IsDeleted == false);
        }

        if (total < 1) return null;
        query = query.Skip(skip).Take(pageSize);

        if (lang != null)
        {
            query = query.Include(tranc1 => tranc1.HospitalTranslations
                         .Where(post => post.LangCode == lang)
                         .OrderBy(post => post.Name));
        }
        else
        {
            query = query
                .Include(tranc2 => tranc2.HospitalTranslations)
                .Include(y => y.HospitalPhoneNumbers);
        }

        await query.ToListAsync();

        var all = new AllHospitalsDto();
        var result = HospitalDto.ToList(query);
        all.Total = total;
        all.Page = page;
        all.PageSize = pageSize;
        all.Hospitals = result.ToList();
        return all;
    }

    public async Task<Response<List<HospitalTranslation>>> SearchByName(string name)
    {
        IQueryable<HospitalTranslation> query = Context.HospitalTranslations;

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(t => t.Name.Contains(name));
        }

        List<HospitalTranslation> results = await query.ToListAsync();

        return new Response<List<HospitalTranslation>>
        {
            Success = true,
            Value = results
        };
    }

    public async Task<Response<List<HospitalDto>>> SearchByCodeNumber(string codeNumber, string lang)
    {
        IQueryable<Hospital> query = Context.Hospitals;

        if (!string.IsNullOrEmpty(codeNumber))
        {
            query = query.Where(t => t.CodeNumber.Contains(codeNumber));
        }
        query = query.Include(t => t.HospitalTranslations.Where(l => l.LangCode == lang));

        List<HospitalDto> results = HospitalDto.ToList(await query.ToListAsync());

        return new Response<List<HospitalDto>>
        {
            Success = true,
            Value = results
        };
    }

    #endregion

    #region Delete
    public async Task<Response> DeleteTranslat(int[] ids)
    {
        try
        {
            foreach (var item in ids)
            {
                var current = await Context.HospitalTranslations.Where(t => t.Id == item).FirstOrDefaultAsync();
                if (current != null)
                {
                    Context.Remove(current);
                    ids[0] = current.HospitalId;
                }
            }
            var rowEffict = await Context.SaveChangesAsync();
            if (rowEffict > 0) return new Response(true, $"delete translate: ids of hosId: {ids[0]} ");

            return new Response(false, $"Id : {ids[0]} is not found  "); ;
        }
        catch (Exception ex)
        {
            return new Response(false, ex.Message);
        }
    }

    public async Task<Response> DeletePhons(int[] ids)
    {
        try
        {
            foreach (var item in ids)
            {
                var current = await Context.HospitalPhoneNumbers.Where(t => t.Id == item).FirstOrDefaultAsync();
                if (current != null)
                {
                    Context.Remove(current);
                    ids[0] = current.HospitalId;
                }
            }
            var rowEffict = await Context.SaveChangesAsync();
            if (rowEffict > 0) return new Response(true, $"delete phons: ids of hosId: {ids[0]} ");

            return new Response(false, $"Id : {ids[0]} is not found  "); ;
        }
        catch (Exception ex)
        {
            return new Response(false, ex.Message);
        }
    }
    #endregion



    //public async Task<Hospital> CreateWithImage(Hospital entity, ImageInputModel image)
    //{
    //    //var imageName = await imageService.SaveSingleImage(image);
    //    //entity.Photo = imageName;
    //   // await Context.Hospitals.AddAsync(entity);
    //    //await Context.SaveChangesAsync();
    //    return entity;
    //}


    //public async Task<IEnumerable<HospitalDto>> GetHospitalTranslations()
    //{

    //    // we should add enum for language*
    //    // we should receive hospital_id form parameter
    //    // try make Auto mapper
    //    var hos = await (from p in Context.Hospitals
    //                     join t in Context.HospitalTranslations on p.Id equals t.HospitalId
    //                     join l in Context.Languages on t.LangCode equals l.Code
    //                     where t.HospitalId == 4 && t.LangCode == "ar"
    //                     select new HospitalDto
    //                     {
    //                         //Name = t.Name,
    //                         //Address = t.Address,
    //                         //Photo = p.Photo,
    //                         //CreateOn = p.CreateOn
    //                     }).ToListAsync();

    //    return hos;
    //}

    //public async Task<IEnumerable<HospitalDto>> GetAllHospitals(string lang)
    //{
    //    var hospitals = await Context.Hospitals
    //        .Include(x => x.HospitalTranslations)
    //        .Include( y => y.HospitalsContactData.Where(x=>x.Id==1))
    //        .ToListAsync();
    //    return HospitalDto.ToList(hospitals);
    //}
}