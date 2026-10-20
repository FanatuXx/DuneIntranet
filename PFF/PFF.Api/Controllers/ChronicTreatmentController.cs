using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.ChronicTreatment;
using PFF.Domain.Queries.ChronicTreatments;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/treatments")]
    public class ChronicTreatmentController : ControllerBase
    {
        private readonly IChronicTreatmentRepository _chronicTreatmentRepository;

        public ChronicTreatmentController(IChronicTreatmentRepository chronicTreatmentRepository)
        {
            _chronicTreatmentRepository = chronicTreatmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _chronicTreatmentRepository.HandleAsync(new GetChronicTreatmentsQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _chronicTreatmentRepository.HandleAsync(new GetChronicTreatmentDetailsQuery(id));
            return this.FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChronicTreatmentDto dto)
        {
            Result result = await _chronicTreatmentRepository.HandleAsync(new CreateChronicTreatmentCommand(
                dto.Dosage,
                dto.PatientId,
                dto.PathologyId,
                dto.MedicineIds), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/treatment", null);
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateChronicTreatmentDto dto)
        {
            Result result = await _chronicTreatmentRepository.HandleAsync(new UpdateChronicTreatmentCommand(
                id,
                dto.Dosage,
                dto.PatientId,
                dto.PathologyId,
                dto.MedicineIds), CancellationToken.None);

            return this.FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _chronicTreatmentRepository.HandleAsync(new DeleteChronicTreatmentCommand(id), CancellationToken.None);
            return this.FromResult(result);
        }
    }
}
