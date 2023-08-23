using DomainModel.Models;
using DomainModel.Models.DashboardStatisticsModels;

namespace DomainModel.Interfaces;

public interface IStatisticsRepository : IGenericRepository
{
    Task<Response<NumberBookingsYear>?> BookingStatistics(int? year);
    Task<Response<EntityCounts>?> FetchCSDPCounts();
}
