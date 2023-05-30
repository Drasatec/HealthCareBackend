using DataAccess.Contexts;
using DataAccess.Repositories;
using DataAccess.UnitOfWorks;
using DomainModel.Models.Hospitals;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrasatHeathApi", Version = "v1" });
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "DrasatHeathApi", Version = "v2" });
        });
        Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
       // Services.AddTransient(typeof(IHospitalRepository), typeof(HospitalRepository));
        Services.AddTransient(typeof(IBaseRepository<HospitalDto>), typeof(HospitalRepository));

        return Services;
    }

    public static IServiceCollection RegisterDbContext(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddDbContext<AppDbContext>(options => options
        .UseSqlServer(Configuration.GetConnectionString("SomeeDb"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        return Services;
    }
}
