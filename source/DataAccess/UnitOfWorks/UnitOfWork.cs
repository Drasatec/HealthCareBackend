using DataAccess.Contexts;
using DataAccess.Repositories;
using DomainModel.Interfaces;

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
        public IGenericRepository RoomTypes { get; private set; }
        public IGenericRepository TypesVisits { get; private set; }
        public IGenericRepository WorkingPeriods { get; private set; }
        public IGenericRepository Generic { get; private set; }
        public IDoctorRepository Doctors { get; private set; }

        public IClientRepository HosClients { get; private set; }
        public IPatientRepository Patients { get; private set; }


        public UnitOfWork(AppDbContext context )
        {
            Context = context;
            Buildings = new BuildingRepository(Context);
            Hospitals = new HospitalRepository(Context);
            Floors = new FloorRepository(Context);
            Rooms = new RoomRepository(Context);
            MedicalSpecialteis = new MedicalSpecialtyRepository(Context);
            Clinics = new ClinicRepository(Context);
            RoomTypes = new GenericRepository(Context);
            TypesVisits = new GenericRepository(Context);
            WorkingPeriods = new GenericRepository(Context);
            Generic = new GenericRepository(Context);
            Doctors = new DoctorRepository(Context);
            HosClients = new ClientRepository(context);
            Patients = new PatientRepository(context);
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
