using DomainModel.Entities;
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

        int Complete();
    }
}
