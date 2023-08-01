using DataAccess.Contexts;
using DomainModel.Entities.Users;
using DomainModel.Interfaces;
namespace DataAccess.Repositories;

public class UserRepository : GenericRepository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public string Test()
    {
        return "done";
    }
    //public async Task<User?> FindByEmailAsync(string email)
    //{
    //    Context.HosUsers
    //}

}