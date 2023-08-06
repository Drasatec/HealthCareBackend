using DomainModel.Entities.Users;
using DomainModel.Models;

namespace DomainModel.Interfaces;

public interface IUserRepository: IGenericRepository
{
    Task<Response> CreateAsync(User entity, string password);
        /// <summary>
    /// this method return all property in user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindById(string userId);
    Task<bool> IsEmailExistAsync(string email);
    Task<User?> ReadUserIdByEmailAsync(string email);
    public string Test();
    Task<bool> UpdateVerificationCode(User user);
}
