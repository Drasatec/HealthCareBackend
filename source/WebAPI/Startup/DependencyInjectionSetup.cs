﻿using DataAccess.Contexts;
using DataAccess.Repositories;
using DataAccess.Services;
using DataAccess.UnitOfWorks;
using DomainModel.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Services;

namespace WebAPI.Startup;
public static class DependencyInjectionSetup
{
    public static IServiceCollection RegisterServices(this IServiceCollection Services)
    {
        //cfg =>
        //{
        //    cfg.Filters.Add(typeof(ExceptionFilter));
        //}
        Services.AddControllers();//.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        Services.AddEndpointsApiExplorer();
        Services.AddSwaggerGen(c =>
        {
            //c.EnableAnnotations(); //[SwaggerOperation(Tags = new[] { "Update" })]
            c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.AttributeRouteInfo?.Order}");
           // c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.AttributeRouteInfo?.Order}_{apiDesc.ActionDescriptor.AttributeRouteInfo?.Name}");
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "DrasatHeathApi", Version = "v2" });
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrasatHeathApi", Version = "v1" });
        });

        Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        Services.AddScoped<IAuthService, AuthService>();
        Services.AddTransient<IMailingService, MailingService>();
        Services.AddTransient<ISMSService, SMSService>();
        Services.AddScoped<IPasswordHasher, PasswordHasher>();
        Services.AddScoped<IUserRepository, UserRepository>();

        return Services;
    }

    public static IServiceCollection RegisterDbContext(this IServiceCollection Services, IConfiguration Configuration)
    {
        // LocalDb ||| SomeeDb
        Services.AddDbContext<AppDbContext>(options => options
        .UseSqlServer(Configuration.GetConnectionString("SomeeDb"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        return Services;
    }
}
