using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QMangaAPI.Data;
using QMangaAPI.Repositories;
using QMangaAPI.Repositories.Context;
using QMangaAPI.Repositories.Impl;
using QMangaAPI.Services;
using QMangaAPI.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
  options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IUserValidatorService, UserValidatorService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

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

SeedData.Seed(app);

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