using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RailWorkshop.API.Utils;
using RailWorkshop.Db;
using RailWorkshop.Db.Data;
using RailWorkshop.Services.Contracts;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Конфигурация авторизации по Bearer токену 
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

string secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
string issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
string audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        IssuerSigningKey = signingKey,
        ValidateIssuerSigningKey = true
    };
});

// Добавление контролов и репизиториев
builder.Services.AddControllers();
builder.Services.AddScoped<IHandbookRepository, HandbookRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStatementRepository, StatementRepository>();

// Подключение базы данных
builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaulConnection")));

// Установка конфигурации Serilog
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// Добавление автомапера
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
