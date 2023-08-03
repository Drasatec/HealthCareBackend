using DataAccess.Contexts;
using DataAccess.Services;
using DomainModel.Entities.Users;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public class UserRepository : GenericRepository, IUserRepository
{
    private readonly IPasswordHasher passwordHasher;
    private readonly IMailingService mailingService;
    private readonly ISMSService smsService;

    public UserRepository(AppDbContext context, IPasswordHasher _passwordHasher, IMailingService mailingService, ISMSService smsService) : base(context)
    {
        passwordHasher = _passwordHasher;
        this.mailingService = mailingService;
        this.smsService = smsService;
    }

    public string Test()
    {
        return "done";
    }

    public async Task<Response<UserRegisterDto>> CreateAsync(UserRegisterDto model)
    {
        Response<UserRegisterDto> result;

        try
        {
            if (IsEmailExist(model.Email))
            {
                result = new Response<UserRegisterDto>(false, "email is exist", model);
            }
            else
            {
                User entity = model;

                // (1) hash password
                entity.PasswordHash = passwordHasher.HashPassword(model.Password);

                // (2) Get what will be confirmed

                // (3) send code verificaiton
                var verificationCode = Helper.VerificationCode();
                if (true)
                {
                    // IsEmail?
                   // _ = mailingService.SendVerificationCodeAsync(model.Email, verificationCode);
                }
                //else
                {
                    // IsSMS?
                   // _ = smsService.SendVerificationCodeAsync(model.PhoneNumber, verificationCode);
                }

                // (4) seve code & expiration_time  in database
                entity.VerificationCode = verificationCode;
                entity.ExpirationTime = DateTimeOffset.Now.AddMinutes(Constants.VerificationCodeMinutesExpires).UtcDateTime;

                // (5) add rols
                //entity.UserRoles.Add(new UserRole() { });

                var entityEntry = await Context.Users.AddAsync(entity);
                if (entityEntry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    result = new Response<UserRegisterDto>(true, null, model);
                    await Context.SaveChangesAsync();
                }
                else
                {
                    return new Response<UserRegisterDto>(false, entityEntry.State.ToString(), model);
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            var exceptionMasseage = $"Message:{ex.Message} \n InnerException: {ex.InnerException?.Message}";
            return new Response<UserRegisterDto>(false, exceptionMasseage, null);
        }
    }

   

    public Task<bool> IsEmailExistAsync(string email)
    {
        return Task.FromResult(IsEmailExist(email));
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        try
        {
            return await Context.Users.Where(x => x.Email == email.ToLower()).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            return null;
        }
        //
        //return await GenericReadById<User>(u => u.Email.Equals(email), null);
    }

    public async Task<User?> FindById(string userId)
    {
        
        return await GenericReadById<User>(u => u.Id == userId, null);

    }

    public async Task<UserVerificationEmailModel?> ReadVerificationCode(string userId)
    {
        Expression<Func<User, bool>> filter = u => u.Id == userId;

        return await GenericReadSingle(filter, (u) => new UserVerificationEmailModel
        {
            Email = u.Id,
            VerificationCode = u.VerificationCode,
            ExpirationTime = u.ExpirationTime,

        });
    }


    //public virtual Task<User?> FindByNameAsync(string userId)
    //{

    //}

    public async Task<bool> CheckPassword(string password, string hashedPassword)
    {
        return await Task.FromResult(passwordHasher.VerifyPassword(password, hashedPassword));
    }


    // private methods
    private bool IsEmailExist(string email)
    {
        return Context.Users.Any(e => e.NormalizedEmail == email.ToLower());
    }

    //public virtual async Task<IdentityResult> ConfirmEmailAsync(TUser user, string token)
    //{

    //}

    //public virtual async Task<bool> IsEmailConfirmedAsync(TUser user)
    //{

    //}


    //public virtual async Task<IdentityResult> ChangeEmailAsync(TUser user, string newEmail, string token)

}