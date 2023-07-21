﻿using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Dtos;
using DomainModel.Models.Patients;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PatientRepository : GenericRepository, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<ResponseId> CreateWithImage(PatientDto dto, Stream? image = null)
    {
        var entity = (Patient)dto;

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

            var result = await Context.Patients.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.MedicalFileNumber))
                entity.MedicalFileNumber = "patient-" + Context.Patients.Count().ToString();
            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new ResponseId(true, "created ", result.Entity.Id);
            }
            return new ResponseId(false, "No row effected ", 0);
        }
        catch (Exception ex)
        {
            return new ResponseId(false, ex.Message + "____and____" + ex.InnerException?.Message, 0);
        }
    }
    #endregion


    #region Update
    public async Task<Response<PatientDto?>> Update(PatientDto dto, int id, Stream? image = null)
    {
        Response<PatientDto?> respons;
        try
        {
            var current = Context.Patients.Find(id);
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
    public async Task<PatientDto?> ReadById(int? Id, string? lang = null)
    {
        var _context = Context.Patients.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.PatientTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.PatientTranslations.Where(la => la.LangCode == lang));
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


    public async Task<PagedResponse<PatientDto>?> ReadAll(int? baseid, bool? appearance, string? status, string? lang, int? pageSize, int? page)
    {

        IQueryable<Patient> query = Context.Patients;

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

        query = query.OrderByDescending(o => o.Id);

        totalCount = query.Count();
        if (totalCount < 0)
            return null;

        // page size
        GenericPagination(ref query, ref pageSize, ref page, totalCount);

        // lang
        if (lang is not null)
        {
            query = query.Include(tranc1 => tranc1.PatientTranslations.Where(post => post.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.PatientTranslations);
        }

        await query.ToListAsync();

        var result = PatientDto.ToList(query);
        var all = new PagedResponse<PatientDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = result.ToList()
        };

        return all;
    }


    public async Task<PagedResponse<PatientDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize)
    {
        var query = from h in Context.Patients
                    join t in Context.PatientTranslations on h.Id equals t.PatientId
                    where (h.MedicalFileNumber.Contains(searchTerm) || t.FullName.Contains(searchTerm))
                          && t.LangCode == lang
                    select new PatientDto
                    {
                        Id = h.Id,
                        Photo = h.Photo,
                        // IsDeleted = h.IsDeleted,
                        NationalityId = h.NationalityId,
                        //  Address = h.Address,
                        BirthDate = h.BirthDate,
                        MaritalStatus = h.MaritalStatus,
                        // Ssn = h.Ssn,
                        //NationalId = h.NationalId,
                        BloodType = h.BloodType,
                        //PatientStatus = h.PatientStatus,
                        //PatientStatus = h.PatientStatus,
                        // ClientGroupId = h.ClientGroupId,
                        // ClientId = h.ClientId,
                        Gender = h.Gender,
                        MedicalFileNumber = h.MedicalFileNumber,
                        PhoneNumber = h.PhoneNumber,
                        //SsntypeId = h.SsntypeId,
                        PatientTranslations = new List<PatientTranslation> { t }
                    };

        var totalCount = await query.CountAsync();

        GenericPagination(ref query, ref pageSize, ref page, totalCount);

        var listDto = await query.OrderByDescending(h => h.Id)
                                 .ToListAsync();

        var all = new PagedResponse<PatientDto>
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