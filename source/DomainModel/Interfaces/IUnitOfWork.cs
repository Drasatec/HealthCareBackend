﻿using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHospitalRepository Hospitals { get; }

        IBuildingRepository Buildings { get; }

        IFloorRepository Floors { get; }

        IRoomRepository Rooms { get; }

        IMedicalSpecialtyRepository MedicalSpecialteis { get; }

        IClinicRepository Clinics { get; }
        IGenericRepository RoomTypes { get; }
        IGenericRepository TypesVisits { get; }
        IGenericRepository WorkingPeriods { get; }
        IGenericRepository Generic { get; }
        IDoctorRepository Doctors { get; }
        IClientRepository HosClients { get; }
        IPatientRepository Patients { get; }
        IAppointmentRepository Appointments { get; }
        //IUserRepository Users { get; }

        int Complete();
    }
}
