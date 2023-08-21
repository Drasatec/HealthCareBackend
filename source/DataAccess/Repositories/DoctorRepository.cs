using DataAccess.Contexts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Doctors;
using DomainModel.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using DomainModel.Entities.DoctorEntities;

namespace DataAccess.Repositories;

public class DoctorRepository : GenericRepository, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<Response<DoctorDto>> CreateWithImage(DoctorDto dto, Stream? image = null)
    {
        var entity = (Doctor)dto;

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

            var result = await Context.Doctors.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
                entity.CodeNumber = "entity-" + Context.Doctors.Count().ToString();
            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new Response<DoctorDto>(true, "created ", result.Entity);
            }
            return new Response<DoctorDto>(false, "No row effected ", null);
        }
        catch (Exception ex)
        {
            return new Response<DoctorDto>(false, ex.Message + "____and____" + ex.InnerException?.Message, null);
        }
    }

    #endregion


    #region Update
    public async Task<Response<DoctorDto?>> Update(DoctorDto dto, int id, Stream? image = null)
    {
        Response<DoctorDto?> respons;
        try
        {
            var current = Context.Doctors.Find(id);
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
            //Context.Entry(current).Property(p => p).IsModified = false;
            await Context.SaveChangesAsync();
            return respons = new(true, $"update on id: {id}", null);
        }
        catch (Exception ex)
        {
            return respons = new(false, "can not duplicate foreignKey with same Id ......." + ex.Message + ex.InnerException?.Message, null);
        }
    }

    public async Task<Response<DoctorDto?>> UpdateAttachment(DoctorAttachment docAttachmnet, string ext, Stream? file = null)
    {
        Response<DoctorDto?> respons;
        try
        {
            var current = Context.DoctorAttachments.Find(docAttachmnet.Id);
            if (current == null)
                return respons = new(false, $"id: {docAttachmnet.Id} is not found");

            var fileName = "";
            var modfied = false;

            if (file != null)
            {
                // if file in database is null
                if (string.IsNullOrEmpty(current.AttachFileName))
                {
                    fileName = Helper.GenerateFileName(ext);
                    _ = FileService.SaveSingleFile(file, fileName);
                    docAttachmnet.AttachFileName = fileName;
                    modfied = true;
                }
                else
                {
                    _ = FileService.UpdateSingleFile(file, current.AttachFileName);
                    modfied = false;
                }
            }
            current = docAttachmnet;
            Context.Update(current).Property(propa => propa.AttachFileName).IsModified = modfied;
            Context.Entry(current).Property(p => p.CreateOn).IsModified = false;
            await Context.SaveChangesAsync();

            return respons = new(true, $"update on id: {docAttachmnet.Id}", null);
        }
        catch (Exception ex)
        {
            return respons = new(false, "can not duplicate foreignKey with same Id ......." + ex.Message + ex.InnerException?.Message, null);
        }
    }
    #endregion


    #region Read
    public async Task<DoctorDto?> ReadById(int? Id, string? lang = null)
    {
        var _context = Context.Doctors.Where(x => x.Id == Id);
        if (lang == null)
        {
            _context = _context.Include(tranc => tranc.DoctorTranslations);
        }
        else
        {
            _context = _context.Include(tranc => tranc.DoctorTranslations.Where(la => la.LangCode == lang));
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

    public async Task<List<DoctorTranslation>> ReadDoctorsNames(int? hosId, int? specialtyId, string lang, int? page, int? pageSize)
    {
        IQueryable<DoctorTranslation> query = Context.DoctorTranslations;
        query = query.Where(l => l.LangCode == lang);
        
        if (hosId.HasValue)
        {
            query = query.Where(s => s.Doctor != null && s.Doctor.DoctorsWorkHospitals.Any(i => i.HospitalId == hosId));
        }

        if (specialtyId.HasValue)
        {
            query = query.Where(f => f.Doctor != null && f.Doctor.SpecialtiesDoctors.Any(s => s.MedicalSpecialtyId == specialtyId));
        }

        if (page.HasValue && pageSize.HasValue)
        {
            GenericPagination(ref query, ref pageSize, ref page);
        }

        return await query.ToListAsync();
    }

    public async Task<PagedResponse<DoctorDto>?> ReadAll(int? hosId, int? specialtyId, bool? appearance, string? status, string? lang, int? pageSize, int? page)
    {

        IQueryable<Doctor> query = Context.Doctors;

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
                query = query.Where(h => h.IsAppearanceOnSite);
            else
                query = query.Where(h => !h.IsAppearanceOnSite);
        }

        if (specialtyId.HasValue)
        {
            query = query.Where(s => s.SpecialtiesDoctors.Any(i => i.MedicalSpecialtyId == specialtyId));
        }

        if (hosId.HasValue)
        {
            query = query.Where(s => s.DoctorsWorkHospitals.Any(i => i.HospitalId == hosId));
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
            query = query.Include(tranc1 => tranc1.DoctorTranslations.Where(post => post.LangCode == lang));
        }
        else
        {
            query = query.Include(tranc2 => tranc2.DoctorTranslations);
        }

        await query.ToListAsync();

        var result = DoctorDto.ToList(query);
        var all = new PagedResponse<DoctorDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = result.ToList()
        };

        return all;
    }


    public async Task<PagedResponse<DoctorDto>?> SearchByNameOrCode(bool? isActive, string searchTerm, string lang, int? page, int? pageSize)
    {
        var query = from h in Context.Doctors
                    join t in Context.DoctorTranslations on h.Id equals t.DoctorId
                    where (h.CodeNumber.Contains(searchTerm) || t.FullName.Contains(searchTerm))
                          && t.LangCode == lang
                    select new DoctorDto
                    {
                        Id = h.Id,
                        Photo = h.Photo,
                        CodeNumber = h.CodeNumber,
                        IsDeleted = h.IsDeleted,
                        DocStatus = h.DocStatus,
                        DoctorsDegreeId = h.DoctorsDegreeId,
                        Gender = h.Gender,
                        IsAppearanceOnSite = h.IsAppearanceOnSite,
                        NationalityId = h.NationalityId,
                        PhoneNumber = h.PhoneNumber,
                        PhoneNumberAppearance = h.PhoneNumberAppearance,
                        Reason = h.Reason,
                        VisitPriceAppearance = h.VisitPriceAppearance,
                        WorkingHours = h.WorkingHours,
                        DoctorTranslations = new List<DoctorTranslation> { t },
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

        var all = new PagedResponse<DoctorDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = listDto
        };

        return all;
    }


    public async Task<List<DoctorVisitPriceDto?>> ReadDoctorVisitPrices(int? id, int? docId, int? priceCategoryId, int? typeVisitId, int? price, string lang)
    {
        var query = from h in Context.DoctorVisitPrices

                    join dt in Context.DoctorTranslations on h.DoctorId equals dt.DoctorId
                    where dt.LangCode == lang

                    join pc in Context.PriceCategoryTranslations on h.PriceCategoryId equals pc.PriceCategoryId
                    where pc.LangCode == lang

                    join tv in Context.TypesVisitTranslations on h.TypeVisitId equals tv.TypeVisitId
                    where tv.LangCode == lang

                    select new DoctorVisitPriceDto
                    {
                        Id = h.Id,
                        DoctorId = h.DoctorId,
                        PriceCategoryId = h.PriceCategoryId,
                        Price = h.Price,
                        TypeVisitId = h.TypeVisitId,
                        PriceCurrency = h.PriceCurrency,

                        DoctorName = dt.FullName,
                        PriceCategory = pc.Name,
                        TypeVisit = tv.Name,

                    };

        if (id.HasValue)
        {
            query = query.Where(i => i.Id.Equals(id));
        }

        if (docId.HasValue)
        {
            query = query.Where(d => d.DoctorId.Equals(docId));
        }

        if (priceCategoryId.HasValue)
        {
            query = query.Where(pc => pc.PriceCategoryId.Equals(priceCategoryId));
        }

        if (typeVisitId.HasValue)
        {
            query = query.Where(t => t.TypeVisitId.Equals(typeVisitId));
        }

        if (price.HasValue)
        {
            query = query.Where(p => p.Price >= price);
        }

        return await query.ToListAsync();
    }


    public async Task<List<PeriodWorkDoctorClinicDto>> ReadDoctorWorkPeriod(int? id, int? docId, int? hosId, int? clinicId, int? periodId, byte? day, string? lang)
    {
        IQueryable<PeriodWorkDoctorClinicDto> query;
        if (lang == null)
            query = Context.DoctorWorkPeriods.Select((h) => new PeriodWorkDoctorClinicDto
            {
                Id = h.Id,
                DoctorId = h.DoctorId,
                ClinicId = h.ClinicId,
                HospitalId = h.HospitalId,
                WorkingPeriodId = h.WorkingPeriodId,
                OnDay = h.OnDay,
            });
        else
            query = from h in Context.DoctorWorkPeriods

                    join dt in Context.DoctorTranslations on h.DoctorId equals dt.DoctorId
                    where dt.LangCode == lang

                    join hos in Context.HospitalTranslations on h.HospitalId equals hos.HospitalId
                    where hos.LangCode == lang

                    join ct in Context.ClinicTranslations on h.ClinicId equals ct.ClinicId
                    where ct.LangCode == lang

                    join wpt in Context.WorkingPeriodTranslations on h.WorkingPeriodId equals wpt.WorkingPeriodId
                    where wpt.LangCode == lang

                    join dy in Context.WeekdaysTranslations on h.OnDay equals dy.WeekdayId
                    where dy.LangCode == lang

                    select new PeriodWorkDoctorClinicDto
                    {
                        Id = h.Id,
                        DoctorId = h.DoctorId,
                        ClinicId = h.ClinicId,
                        HospitalId = h.HospitalId,
                        WorkingPeriodId = h.WorkingPeriodId,
                        OnDay = h.OnDay,

                        DayName = dy.Name,
                        Doctor = dt.FullName,
                        Hospital = hos.Name,
                        Clinic = ct.Name,
                        WorkingPeriod = wpt.Name,
                    };

        if (id.HasValue)
        {
            query = query.Where(i => i.Id.Equals(id));
        }
        else
        {
            if (docId.HasValue)
            {
                query = query.Where(d => d.DoctorId.Equals(docId));
            }

            if (hosId.HasValue)
            {
                query = query.Where(pc => pc.HospitalId.Equals(hosId));
            }

            if (clinicId.HasValue)
            {
                query = query.Where(t => t.ClinicId.Equals(clinicId));
            }

            if (periodId.HasValue)
            {
                query = query.Where(w => w.WorkingPeriodId.Equals(periodId));
            }

            if (day.HasValue)
            {
                query = query.Where(p => p.OnDay.Equals(day));
            }
        }

        return await query.OrderByDescending(h => h.Id).ToListAsync();
    }


    public async Task<PagedResponse<DoctorWorkPeriodDto>?> FindDoctor(int? hosId, int? specialtyId, int? docId, int? workingPeriodId, byte? day, short? doctorsDegreeId, byte? gender, int? page, int? pageSize, string? lang)
    {
        IQueryable<DoctorWorkPeriod> query = Context.DoctorWorkPeriods;
        IQueryable<DoctorWorkPeriodDto> result;

        var totalCount = 0;

        if (hosId.HasValue)
        {
            query = query.Where(d => d.HospitalId.Equals(hosId));
        }
        if (specialtyId.HasValue)
        {
            query = query.Where(d => d.SpecialtyId.Equals(specialtyId));
        }
        if (docId.HasValue)
        {
            query = query.Where(d => d.DoctorId.Equals(docId));
        }

        if (workingPeriodId.HasValue)
        {
            query = query.Where(d => d.WorkingPeriodId.Equals(workingPeriodId));
        }
        if (day.HasValue)
        {
            query = query.Where(d => d.OnDay.Equals(day));
        }

        if (doctorsDegreeId.HasValue)
        {
            query = query.Where(d => d.Doctor != null && d.Doctor.DoctorsDegreeId.Equals(doctorsDegreeId));
        }

        if (gender.HasValue)
        {
            query = query.Where(d => d.Doctor != null && d.Doctor.Gender.Equals(gender));
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
            query = query.Include(d1 => d1.Doctor).ThenInclude(td => td.DoctorTranslations.Where(l => l.LangCode == lang));

            result = from h in query

                     join hos in Context.HospitalTranslations on h.HospitalId equals hos.HospitalId
                     where hos.LangCode == lang

                     join wpt in Context.WorkingPeriodTranslations on h.WorkingPeriodId equals wpt.WorkingPeriodId
                     where wpt.LangCode == lang

                     select new DoctorWorkPeriodDto
                     {
                         doctorWorkPeriod = new PeriodWorkDoctorClinicDto
                         {
                             Id = h.Id,
                             HospitalId = h.HospitalId,
                             SpecialtyId = h.SpecialtyId,
                             ClinicId = h.ClinicId,
                             DoctorId = h.DoctorId,
                             WorkingPeriodId = h.WorkingPeriodId,
                             OnDay = h.OnDay,
                             WorkingPeriod = wpt.Name,
                             Hospital = hos.Name
                         },
                         Doctor = h.Doctor
                     };
        }
        else
        {
            query = query.Include(d2 => d2.Doctor);

            result = from h in query
                     select new DoctorWorkPeriodDto
                     {
                         doctorWorkPeriod = new PeriodWorkDoctorClinicDto
                         {
                             Id = h.Id,
                             HospitalId = h.HospitalId,
                             SpecialtyId = h.SpecialtyId,
                             ClinicId = h.ClinicId,
                             DoctorId = h.DoctorId,
                             WorkingPeriodId = h.WorkingPeriodId,
                             OnDay = h.OnDay,
                         },
                         Doctor = h.Doctor
                     };
        }

        await query.ToListAsync();

        //result = DoctorWorkPeriodDto.ToList(query);
        var all = new PagedResponse<DoctorWorkPeriodDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = result.ToList()
        };

        return all;

    }
    #endregion

}