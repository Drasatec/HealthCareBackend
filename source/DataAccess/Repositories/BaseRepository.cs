using DomainModel.Entities;
using DomainModel.Interfaces;
using DomainModel.Models.Hospitals;

namespace DataAccess.Repositories;

public class BaseRepository<T> :  IBaseRepository<T> where T : class
{
    public Task<T?> AddTranslations(List<HospitalTranslation> dto, int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> Create(T dto)
    {
        throw new NotImplementedException();
    }

    public Task<T?> CreateWithImage(T dto, Stream? image = null)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> ReadAll(bool isOrder, string? lang, int page = 1, int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    public Task<T?> ReadSingleById(int Id, string? lang = null)
    {
        throw new NotImplementedException();
    }

    public Task<T?> Update(T dto, int id, Stream? image = null)
    {
        throw new NotImplementedException();
    }

    public Task<string?> UpdateAnImage(Stream image, int id)
    {
        throw new NotImplementedException();
    }
}
