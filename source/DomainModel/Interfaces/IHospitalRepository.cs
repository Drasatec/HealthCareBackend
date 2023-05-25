using DomainModel.Entities;
using DomainModel.Models;
using DomainModel.Models.Hospitals;

namespace DomainModel.Interfaces;

public interface IHospitalRepository : IGenericRepository<Hospital>
{
    Task<Hospital> CreateWithImage(Hospital entity, ImageInputModel image);
    Task<IEnumerable<HospitalDto>> GetAllHospitals(string lang);
    Task<IEnumerable<HospitalDto>> GetHospitalTranslations();
}
