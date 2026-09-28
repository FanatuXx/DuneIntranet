using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PFF.Domain;
using PFF.Domain.Repositories;
using PFF.Domain.Services;
using Scalar.AspNetCore;
using System.Data.Common;
using System.Text.Json.Serialization;

string policyName = "CorsicanPoliceDepartment";

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //});

builder.Services.AddCors(c => c.AddPolicy(policyName, o =>
{
    o.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    o.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dune")));

builder.Services.AddScoped<IPathologyRepository, PathologyService>();
builder.Services.AddScoped<IPatientRepository, PatientService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
