using DomainModel.Models.Admins;

namespace DomainModel.Interfaces.Services;

public interface IAdminAuthService
{
    Task<AdminAuthModel> LoginAsync(AdminLogin adminogin);
    public string TestAuth();
}
