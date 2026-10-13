using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PFF.Domain;
using PFF.Domain.Model.Entities;
using PFF.Domain.Repositories;
using PFF.Domain.Services;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

string policyName = "CorsicanPoliceDepartment";

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddCors(c => c.AddPolicy(policyName, o =>
{
    o.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    o.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dune")));

builder.Services.AddScoped<IPathologyRepository, PathologyService>();
builder.Services.AddScoped<IPatientRepository, PatientService>();
builder.Services.AddScoped<IPatientAddressRepository, PatientAddressService>();

builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7050",
            ValidAudience = "https://localhost:7050",
            ClockSkew = TimeSpan.FromMinutes(1),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(policyName);

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
