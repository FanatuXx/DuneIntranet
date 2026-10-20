using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.Consultations;
using PFF.Domain.Queries.Consultations;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/consultations")]
    public class ConsultationController : ControllerBase
    {
        private readonly IConsultationRepository _consultationRepository;

        public ConsultationController(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _consultationRepository.HandleAsync(new GetConsultationsQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _consultationRepository.HandleAsync(new GetConsultationDetailsQuery(id));
            return this.FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsultationDto dto)
        {
            Result result = await _consultationRepository.HandleAsync(new CreateConsultationCommand(
                dto.Motive,
                dto.Subjective,
                dto.Objective,
                dto.Assessment,
                dto.Plan,
                dto.Description,
                dto.Date,
                dto.PatientId,
                dto.Workers,
                dto.Pathologies,
                dto.VitalSigns,
                dto.Prescriptions), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/consultation", null);
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateConsultationDto dto)
        {
            Result result = await _consultationRepository.HandleAsync(new UpdateConsultationCommand(
                id,
                dto.Motive,
                dto.Subjective,
                dto.Objective,
                dto.Assessment,
                dto.Plan,
                dto.Description,
                dto.Date,
                dto.PatientId,
                dto.Workers,
                dto.Pathologies,
                dto.VitalSigns,
                dto.Prescriptions), CancellationToken.None);

            return this.FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _consultationRepository.HandleAsync(new DeleteConsultationCommand(id), CancellationToken.None);
            return this.FromResult(result);
        }
    }
}
