using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Hospitals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class HospitalRepository : GenericRepository<Hospital>, IHospitalRepository
    {
        ImageService imageService = new ImageService();

        public HospitalRepository(AppDbContext context) : base(context) { }


        public async Task<Hospital> CreateWithImage(Hospital entity, ImageInputModel image)
        {
            var imageName = await imageService.SaveSingleImage(image);
            entity.Photo = imageName;
            await Context.Hospitals.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }


        public async Task<IEnumerable<HospitalDto>> GetHospitalTranslations()
        {

            // we should add enum for language*
            // we should receive hospital_id form parameter
            // try make Auto mapper
            var hos = await (from p in Context.Hospitals
                             join t in Context.HospitalTranslations on p.Id equals t.HospitalId
                             join l in Context.Languages on t.LangCode equals l.Code
                             where t.HospitalId == 4 && t.LangCode == "ar"
                             select new HospitalDto
                             {
                                 //Name = t.Name,
                                 //Address = t.Address,
                                 //Photo = p.Photo,
                                 //CreateOn = p.CreateOn
                             }).ToListAsync();

            return hos;
        }

        public async Task<IEnumerable<HospitalDto>> GetAllHospitals(string lang)
        {
            var hospitals = await Context.Hospitals
                .Include(x => x.HospitalTranslations)
                .Include(y => y.HospitalsContactData)
                .ToListAsync();
            return HospitalDto.ToList(hospitals);
        }



    }
}
