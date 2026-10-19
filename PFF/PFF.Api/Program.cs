using Microsoft.EntityFrameworkCore;
using PFF.Domain;
using PFF.Domain.Repositories;
using PFF.Domain.Services;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PFF.Tools.Security;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddCustomSecurity(options =>
{
    options.PolicyName = "PreventDeletePolicy";
    options.UseCors = true;
    options.AddOrigins("http://localhost:4200", "http://127.0.0.1:4200");
    options.SetMethods(HttpVerbs.GET | HttpVerbs.POST | HttpVerbs.PATCH | HttpVerbs.PATCH);
    //options.AddHeaders("id");
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dune")));

builder.Services.AddScoped<IPathologyRepository, PathologyService>();
builder.Services.AddScoped<IPatientRepository, PatientService>();
builder.Services.AddScoped<IPatientAddressRepository, PatientAddressService>();
builder.Services.AddScoped<IChronicTreatmentRepository, ChronicTreatmentService>();
builder.Services.AddScoped<IMedicineRepository, MedicineService>();

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

app.UseCustomSecurity();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
