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

        public IBuildingRepository Buildings { get; private set; }
        public IFloorRepository Floors { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public IMedicalSpecialtyRepository MedicalSpecialteis{ get; private set; }
        public IClinicRepository Clinics { get; private set; }

        public UnitOfWork(AppDbContext context )
        {
            Context = context;
            Buildings = new BuildingRepository(Context);
            Hospitals = new HospitalRepository(Context);
            Floors = new FloorRepository(Context);
            Rooms = new RoomRepository(Context);
            MedicalSpecialteis = new MedicalSpecialtyRepository(Context);
            Clinics = new ClinicRepository(Context);
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
