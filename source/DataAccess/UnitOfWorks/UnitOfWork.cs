using DataAccess.Contexts;
using DataAccess.Repositories;
using DomainModel.Entities;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext Context { get; }

        public IHospitalRepository Hospitals { get; private set; }

        public IGenericRepository<HosBuilding> HosBuilding { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            Context = context;
            HosBuilding = new GenericRepository<HosBuilding>(Context);
            Hospitals = new HospitalRepository(Context);
        }
        public int Complete()
        {
            return Context.SaveChangesAsync().Result;
        }

        public void Dispose()
        {
            Context.Dispose();
        }


    }
}
