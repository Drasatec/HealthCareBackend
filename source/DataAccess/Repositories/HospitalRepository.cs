using DataAccess.Contexts;
using DomainModel.Contracts;
using DomainModel.Entities;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models.Hospitals;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;
public class HospitalRepository : IBaseRepository<HospitalDto> // : GenericRepository<Hospital>, IHospitalRepository
{

    protected AppDbContext Context { get; set; }
    public HospitalRepository(AppDbContext context)
    {
        Context = context;
    }

    public async Task<HospitalDto> Create(HospitalDto Dto)
    {
        var entity = (Hospital)Dto;
        var result = await Context.Hospitals.AddAsync(entity);
        if (entity.HospitalsContactData == null)
        {
            entity.HospitalsContactData = new List<HospitalsContactDatum>(1)
            {
                new() { Id = result.Entity.Id }
            };
        }
        await Context.SaveChangesAsync();
        Dto = result.Entity;
        return Dto;
    }

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
        if (entity.HospitalsContactData.Count() < 1)
        {
            entity.HospitalsContactData = new List<HospitalsContactDatum>(1)
            {
                new() { Id = result.Entity.Id }
            };
        }
        await Context.SaveChangesAsync();

        return await ReadSingleById(result.Entity.Id); //(HospitalDto) result.Entity;
    }

    public async Task<HospitalDto?> AddTranslations(List<HospitalTranslation> dto, int id)
    {
        var mode = new HospitalDto();
        await Context.HospitalTranslations.AddRangeAsync(dto);
        await Context.SaveChangesAsync();
        return await ReadSingleById(id);
    }

    public async Task<HospitalDto?> ReadSingleById(int Id, string? lang = null)
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
                .Include(contact => contact.HospitalsContactData)
                .SingleOrDefaultAsync();
            return entity!;
        }
        catch (ArgumentNullException)
        {
            return null;
        }
    }

    public async Task<HospitalDto?> Update(HospitalDto dto, int id, Stream? image = null)
    {
        try
        {
            var current = Context.Hospitals.Find(id);
            if (current == null)
                return null;
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
            await Context.SaveChangesAsync();
            return await ReadSingleById(id);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<string?> UpdateAnImage(Stream image, int id)
    {
        var current = Context.Hospitals.Find(id);
        if (current == null)
            return null;

        var imageName = "";

        if (string.IsNullOrEmpty(current.Photo))
        {
            imageName = Helper.GenerateImageName();
            _ = DataAccessImageService.SaveSingleImage(image, imageName);
            current.Photo = imageName;
            Context.Update(current).Property(p => p.CodeNumber).IsModified = false;

            Context.Entry(current).Property(p => p.IsDeleted).IsModified = false;
            Context.Entry(current).Property(p => p.CreateOn).IsModified = false;
            await Context.SaveChangesAsync();
        }

        else
        {
            _ = DataAccessImageService.UpdateSingleImage(image, current.Photo);
        }
        return "true";
    }

    public async Task<IEnumerable<HospitalDto>> ReadAll(bool isOrder, string? lang, int page = 1, int pageSize = 10)
    {
        int skip = Helper.SkipValue(page, pageSize);

        IQueryable<Hospital> query = Context.Hospitals;

        query = query.Skip(skip).Take(pageSize);


        if (lang != null)
        {
            query = query
                .Include(tranc1 => tranc1.HospitalTranslations
                .Where(post => post.LangCode == lang)
                .OrderBy(post => post.Name));
        }
        else
        {
            query = query
                .Include(tranc2 => tranc2.HospitalTranslations)
                .Include(y => y.HospitalsContactData);
        }

        await query.ToListAsync();

        return HospitalDto.ToList(query);
    }


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
