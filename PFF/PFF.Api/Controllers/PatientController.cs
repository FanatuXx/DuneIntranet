using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.Patient;
using PFF.Domain.Queries;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

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
        public async Task<IActionResult> Post([FromBody] CreatePatientDto dto)
        {
            Result result = await _patientRepository.HandleAsync(new CreatePatientCommand(
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
                DateTime.Now,
                dto.PatientAddress.Street,
                dto.PatientAddress.Number,
                dto.PatientAddress.ZipCode,
                dto.PatientAddress.Town,
                dto.PatientAddress.Country
                ), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/patient", null);
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePatientDto dto)
        {
            return this.FromResult(_patientRepository.Handle(new UpdatePatientCommand(
                id, //est récupérée depuis l'URL
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
                dto.PatientAddress.Street,
                dto.PatientAddress.Number,
                dto.PatientAddress.ZipCode,
                dto.PatientAddress.Town,
                dto.PatientAddress.Country
                )));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return this.FromResult(_patientRepository.Handle(new DeletePatientCommand(id)));
        }
    }
}
