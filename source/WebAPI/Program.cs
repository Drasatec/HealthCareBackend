using Microsoft.AspNetCore.Mvc.Versioning;
using WebApi.Startup;
using WebAPI.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices();
builder.Services.RegisterDbContext(builder.Configuration);

// Add services to the container.
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;

    config.ApiVersionReader = new HeaderApiVersionReader("drasat-version-key");
    config.ApiVersionReader = new MediaTypeApiVersionReader(); //Content-Type || text/plain;v=2.0

    config.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("drasat-version-key"),
                    new MediaTypeApiVersionReader());
});

builder.Services.AddVersionedApiExplorer(conf =>
{
    conf.GroupNameFormat = "'v'VVV";
    conf.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();
app.UseStaticFiles();
app.UseCors(w => w.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.AppUsingPoints();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
