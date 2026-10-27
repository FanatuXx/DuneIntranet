using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.Consumptions;
using PFF.Domain.Commands.Patient;
using PFF.Domain.Commands.Patients;
using PFF.Domain.Queries.Patients;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _patientRepository.HandleAsync(new GetPatientsQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _patientRepository.HandleAsync(new GetPatientDetailsQuery(id));
            return this.FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
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
                DateTime.Now,
                DateTime.Now,

                dto.PatientAddress != null ? new CreatePatientAddressData(
                    dto.PatientAddress?.Street,
                    dto.PatientAddress?.Number,
                    dto.PatientAddress!.ZipCode,
                    dto.PatientAddress?.Town,
                    dto.PatientAddress?.Country) : null,

                (dto.Consumptions ?? new List<CreateConsumptionDto>())
                    .Select(c => new CreateConsumptionData(
                        c.DrugType,
                        c.Frequency))
                    .ToList()), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/patient", null);
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDto dto)
        {
            Result result = await _patientRepository.HandleAsync(new UpdatePatientCommand(
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

                dto.PatientAddress != null ? new UpdatePatientAddressData(
                    id,
                    dto.PatientAddress?.Street,
                    dto.PatientAddress?.Number,
                    dto.PatientAddress!.ZipCode,
                    dto.PatientAddress?.Town,
                    dto.PatientAddress?.Country) : null,

                (dto.ConsumptionsToUpdate ?? new List<UpdateConsumptionDto>())
                    .Select(c => new UpdateConsumptionData(
                        id,
                        c.DrugType,
                        c.Frequency))
                    .ToList(),

                (dto.ConsumptionsToCreate ?? new List<CreateConsumptionDto>())
                    .Select(c => new CreateConsumptionData(
                        c.DrugType,
                        c.Frequency))
                    .ToList()), CancellationToken.None);

            return this.FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _patientRepository.HandleAsync(new DeletePatientCommand(id), CancellationToken.None);
            return this.FromResult(result);
        }
    }
}
