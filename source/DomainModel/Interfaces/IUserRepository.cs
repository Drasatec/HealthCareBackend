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
    Task<User?> FindByPhoneNumberAsync(string phone);

    //Task<User?> FindByPhoneNumberAsync(string email);
    Task<bool> IsEmailExistAsync(string email);
    Task<bool> IsPhoneExistAsync(string phone);
    /// <summary>
    /// select userId, email, Full name properties in user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<User?> ReadUserIdByEmailAsync(string email);
    /// <summary>
    /// select userId, phone, Full name properties in user by phone
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    Task<User?> ReadUserIdByPhoneAsync(string phone);
    public string Test();
    Task<bool> UpdateVerificationCode(User user);
}
