using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QMangaAPI.Data;
using QMangaAPI.Data.Context;
using QMangaAPI.Data.Interfaces.Repositories;
using QMangaAPI.Data.Interfaces.Services;
using QMangaAPI.Repositories;
using QMangaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
  options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserValidator, UserValidator>();

builder.Services.AddCors(options =>
{
  options.AddPolicy("QMangaPolicy", policy =>
  {
    policy
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});

builder.Services.AddDbContext<AppDbContext>(option =>
{
  option.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});

builder.Services.AddAuthentication(e =>
{
  e.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  e.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(e =>
{
  e.RequireHttpsMetadata = false;
  e.SaveToken = true;
  e.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey("qmangaveryverysecret"u8.ToArray()),
    ValidateAudience = false,
    ValidateIssuer = false,
    ClockSkew = TimeSpan.Zero
  };
});

var app = builder.Build();

Seed.SeedData(app);

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("QMangaPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();