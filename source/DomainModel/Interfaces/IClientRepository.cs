using DomainModel.Models.MedicalSpecialteis;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models.Client;

namespace DomainModel.Interfaces;

public interface IClientRepository : IGenericRepository
{
    Task<ResponseId> CreateWithImage(HotClientDto dto, Stream? image = null);
    Task<PagedResponse<HotClientDto>?> ReadAll(byte? status, int? pageSize, int? page);
    Task<HotClientDto?> ReadById(int? Id);
    Task<PagedResponse<HotClientDto>?> SearchByNameOrCode(byte? status, string searchTerm, int? page, int? pageSize);
    Task<Response<HotClientDto?>> Update(HotClientDto dto, int id, Stream? image = null);
}