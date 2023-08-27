using DomainModel.Models.AppSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Startup;
using WebAPI.Startup;
//  Scaffold-DbContext 'Data Source=pcFawzy\SQLEXPRESS; Initial Catalog=alrahma_care_db;Trusted_Connection=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -OutputDir My_Models2

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices();
builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});
var app = builder.Build();
app.UseCors(w => w.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseStaticFiles();
app.AppUsingPoints();

