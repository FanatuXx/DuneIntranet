using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.FromResult(_patientRepository.Handle(new GetPatientsQuery()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return this.FromResult(_patientRepository.Handle(new GetPatientDetailsQuery(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePatientDto dto)
        {
            Result result = _patientRepository.Handle(new CreatePatientCommand(
                dto.SSIN, 
                dto.IdNumber,
                dto.FirstName,
                dto.LastName,
                dto.Alias,
                dto.Gender,
                dto.BirthDate,
                dto.PhoneNumber,
                dto.Allergies,
                dto.IsInsured,
                dto.Insurance,
                dto.HasInsuranceCard, 
                dto.InsuranceCardEndDate, 
                dto.IsAtFedasil,
                dto.Income, 
                dto.Status, 
                dto.IsWorking, 
                dto.DrugType,
                dto.ConsumptionFrequency,
                DateTime.Now,
                DateTime.Now
                ));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/patient", null);
        }

        [HttpPatch]
        [HttpPut]
        public IActionResult Post([FromBody] UpdatePatientDto dto)
        {
            return this.FromResult(_patientRepository.Handle(new UpdatePatientCommand(
                dto.Id,
                dto.SSIN,
                dto.IdNumber,
                dto.FirstName,
                dto.LastName,
                dto.Alias,
                dto.Gender,
                dto.BirthDate,
                dto.PhoneNumber,
                dto.Allergies,
                dto.IsInsured,
                dto.Insurance,
                dto.HasInsuranceCard,
                dto.InsuranceCardEndDate,
                dto.IsAtFedasil,
                dto.Income,
                dto.Status,
                dto.IsWorking,
                dto.DrugType,
                dto.ConsumptionFrequency
                )));
        }
    }
}
