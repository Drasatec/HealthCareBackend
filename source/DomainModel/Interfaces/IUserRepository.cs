using DomainModel.Entities.Users;
using DomainModel.Models;
using DomainModel.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces;

public interface IUserRepository: IGenericRepository
{
    Task<Response<UserRegisterDto>> CreateAsync(UserRegisterDto model);
    Task<User?> FindByEmailAsync(string email);
    public string Test();
}
