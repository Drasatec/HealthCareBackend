using DataAccess.Contexts;
using DataAccess.UnitOfWorks;
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
            //c.EnableAnnotations(); //[SwaggerOperation(Tags = new[] { "Update" })]
            c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.AttributeRouteInfo?.Order}");
           // c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.AttributeRouteInfo?.Order}_{apiDesc.ActionDescriptor.AttributeRouteInfo?.Name}");
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "DrasatHeathApi", Version = "v2" });
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrasatHeathApi", Version = "v1" });
        });
        Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
       // Services.AddTransient(typeof(IHospitalRepository), typeof(HospitalRepository));
        //Services.AddTransient(typeof(IBaseRepository<HospitalDto>), typeof(HospitalRepository));

        return Services;
    }

    public static IServiceCollection RegisterDbContext(this IServiceCollection Services, IConfiguration Configuration)
    {
        // LocalDb ||| SomeeDb
        Services.AddDbContext<AppDbContext>(options => options
        .UseSqlServer(Configuration.GetConnectionString("LocalDb"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        return Services;
    }
}
