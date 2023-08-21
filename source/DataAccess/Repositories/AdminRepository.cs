using DataAccess.Contexts;
using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.Users;
using DomainModel.Interfaces;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Models.Admins;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AdminRepository: GenericRepository, IAdminRepository
{
    private readonly IPasswordHasher passwordHasher;
    private readonly IMailingService mailingService;
    private readonly ISMSService smsService;

    public AdminRepository(AppDbContext context, IPasswordHasher _passwordHasher, IMailingService mailingService, ISMSService smsService) : base(context)
    {
        passwordHasher = _passwordHasher;
        this.mailingService = mailingService;
        this.smsService = smsService;
    }

    public string Test()
    {
        return "done ";
    }
    public Task<bool> IsUserNameExistAsync(string username) => Context.EmployeeAccounts.AnyAsync(e => e.UserName == username.ToLower());

    public async Task<Response> CreateAsync(EmployeeAccountDto dto)
    {
        EmployeeAccount entity = dto;
        Response result;
        try
        {
            // hash password
            entity.PasswordHash = passwordHasher.HashPassword(dto.Password);

            // add rols
            // write cod for and rols

            var entityEntry = await Context.EmployeeAccounts.AddAsync(entity);

            if (entityEntry.State == EntityState.Added)
            {
                await Context.SaveChangesAsync();
                result = new Response(true, entity.Id);
            }
            else
            {
                return new Response(false, entityEntry.State.ToString());
            }

            return result;
        }
        catch (Exception ex)
        {
            var exceptionMasseage = $"Message:{ex.Message} \n InnerException: {ex.InnerException?.Message}";
            return new Response(false, exceptionMasseage);
        }
    }
    public async Task<EmployeeAccount?> FindByUserNameAsync(string username)
    {
        try
        {
            return await Context.EmployeeAccounts.FirstOrDefaultAsync(x => x.UserName == username.ToLower());
        }
        catch (Exception)
        {
            return null;
        }
    }
}
