using System.Text;
using System.Text.Json.Serialization;
using GestioneAccounts.Abstractions;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;
using GestioneAccounts.DataAccess.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Namespace.GestioneAccounts.Configuration;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));


// 2. Identity
builder.Services.AddIdentity<Account, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. Repositories (Dependency Injection)
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IValoriRepository, ValoriRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// 4. MediatR (CQRS)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// 5. JWT Authentication
var secret = builder.Configuration.GetValue<string>("JwtConfig:Secret")
    ?? throw new InvalidOperationException("JwtConfig:Secret is not configured.");

var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Set true and configure if needed
        ValidateAudience = false, // Set true and add "Audience" in appsettings if needed
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RequireExpirationTime = true
    };
    options.SaveToken = true;
});

// 6. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});



// 7. Controllers + JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// 8. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestione Accounts API", Version = "v1" });
});

// 9. Web root (opzionale)
builder.WebHost.UseWebRoot("wwwroot");
// Registra tutti i profili nell'assembly corrente
builder.Services.AddAutoMapper(typeof(Program));

// oppure: builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// === Middleware pipeline ===
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestione Accounts API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
