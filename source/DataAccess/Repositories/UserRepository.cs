﻿using DataAccess.Contexts;
using DomainModel.Entities;
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

    public async Task<Response> CreateAsync(User entity, string password)
    {
        Response result;
        try
        {
            // hash password
            entity.PasswordHash = passwordHasher.HashPassword(password);

            // ExpirationTime
            entity.ExpirationTime = DateTimeOffset.Now.AddMinutes(Constants.VerificationCodeMinutesExpires).UtcDateTime;

            // add rols
            // write cod for and rols


            var entityEntry = await Context.Users.AddAsync(entity);
            if (entityEntry.State == EntityState.Added)
            {
                result = new Response(true, entity.Id);
                await Context.SaveChangesAsync();
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

    public async Task<ResponseId> CreateWithNewPatientAsync(Patient entity, string password)
    {
        ResponseId result;
        try
        {
            entity.PatientStatus = 1;
            entity.UserAccount.PasswordHash = passwordHasher.HashPassword(password);
            entity.UserAccount.ExpirationTime = DateTimeOffset.Now.AddMinutes(Constants.VerificationCodeMinutesExpires).UtcDateTime;

            // plese set username of patinet , userName == MedicalFileNumber;
            var entityEntry = await Context.Patients.AddAsync(entity);
            if (entityEntry.State == EntityState.Added)
            {
                result = new ResponseId(true, "", entity.Id);
                await Context.SaveChangesAsync();
            }
            else
            {
                return new ResponseId(false, entityEntry.State.ToString(), 0);
            }
            return result;
        }
        catch (Exception ex)
        {
            var exceptionMasseage = $"Message:{ex.Message} \n InnerException: {ex.InnerException?.Message}";
            return new ResponseId(false, exceptionMasseage, 0);
        }
    }

    public Task<bool> IsEmailExistAsync(string email) => Context.UserAccounts.AnyAsync(e => e.Email == email.ToLower());

    public Task<bool> IsPhoneExistAsync(string phone) => Context.UserAccounts.AnyAsync(e => e.PhoneNumber == phone.ToLower());

    public async Task<UserAccount?> FindByEmailAsync(string email)
    {
        try
        {
            return await Context.UserAccounts.FirstOrDefaultAsync(x => x.Email == email.ToLower());
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<UserAccount?> FindByPhoneNumberAsync(string phone)
    {
        try
        {
            return await Context.UserAccounts.FirstOrDefaultAsync(x => x.PhoneNumber == phone.ToLower());
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<UserAccount?> FindById(int userId)
    {
        //return await GenericReadById<User>(u => u.Id == userId, null);
        try
        {
            return await Context.UserAccounts.FirstOrDefaultAsync(x => x.Id == userId);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<User?> ReadUserIdByEmailAsync(string email)
    {
        try
        {
            return await Context.Users
                .Where(x => x.Email == email.ToLower())
                .Select((user) => new User { Id = user.Id, Email = user.Email, VerificationCode = user.VerificationCode, ExpirationTime = user.ExpirationTime, FullName = user.FullName })
                .FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<User?> ReadUserIdByPhoneAsync(string phone)
    {
        try
        {
            return await Context.Users
                .Where(x => x.PhoneNumber == phone.ToLower())
                .Select((user) => new User { Id = user.Id, PhoneNumber = user.PhoneNumber, VerificationCode = user.VerificationCode, ExpirationTime = user.ExpirationTime, FullName = user.FullName })
                .FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            return null;
        }
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

    public async Task<bool> UpdateVerificationCode(User user)
    {
        user.ExpirationTime = DateTimeOffset.Now.AddMinutes(Constants.VerificationCodeMinutesExpires).UtcDateTime;
        Context.Attach(user);
        Context.Entry(user).Property(vc => vc.VerificationCode).IsModified = true;
        Context.Entry(user).Property(et => et.ExpirationTime).IsModified = true;

        return await Context.SaveChangesAsync() > 0 ? true : false;
    }

    public async Task<bool> CheckPassword(string password, string hashedPassword)
    {
        return await Task.FromResult(passwordHasher.VerifyPassword(password, hashedPassword));
    }














    //public async Task<bool> SendVerificaitonCodeToEmail(string email)
    //{
    //    var user = await FindByEmailAsync(email);
    //    if (user != null)
    //    {
    //        var verificationCode = Helper.VerificationCode();
    //        user.VerificationCode = verificationCode;
    //        user.ExpirationTime = DateTimeOffset.Now.AddMinutes(Constants.VerificationCodeMinutesExpires).UtcDateTime;

    //        await UpdateVerificationCode(user);
    //        await mailingService.SendVerificationCodeAsync(email, verificationCode);
    //    }
    //    return true;
    //}

    // private methods


    //public virtual Task<User?> FindByNameAsync(string userId)
    //{

    //}

    //public virtual async Task<IdentityResult> ConfirmEmailAsync(TUser user, string token)
    //{

    //}

    //public virtual async Task<bool> IsEmailConfirmedAsync(TUser user)
    //{

    //}


    //public async Task<User?> FindByPhoneNumberAsync(string phone)
    //{
    //    try
    //    {
    //        return await Context.Users.Where(x => x.PhoneNumber == phone.ToLower()).FirstOrDefaultAsync();
    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }
    //}



    //public virtual async Task<IdentityResult> ChangeEmailAsync(TUser user, string newEmail, string token)

}