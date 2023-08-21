using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.Users;
using DomainModel.Models;
using DomainModel.Models.Admins;

namespace DomainModel.Interfaces;

public interface IAdminRepository
{
    Task<Response> CreateAsync(EmployeeAccountDto dto);
    Task<EmployeeAccount?> FindByUserNameAsync(string username);
    Task<bool> IsUserNameExistAsync(string username);
    string Test();
}
