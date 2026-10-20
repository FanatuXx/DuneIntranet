using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.Workers;
using PFF.Domain.Queries.Workers;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route(("api/workers"))]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkerController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _workerRepository.HandleAsync(new GetWorkersQuery());
            return this.FromResult(result);
        }

        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _workerRepository.HandleAsync(new GetWorkerDetailsQuery(id));
            return this.FromResult(result);
        }

        public async Task<IActionResult> Create([FromBody] CreateWorkerDto dto)
        {
            Result result = await _workerRepository.HandleAsync(new CreateWorkerCommand(
                dto.SSIN,
                dto.FirstName,
                dto.LastName,
                dto.Gender,
                dto.BirthDate,
                dto.Email,
                dto.PhoneNumber,
                dto.Occupation,
                dto.Street,
                dto.Number,
                dto.ZipCode,
                dto.Town,
                dto.Country), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/worker", null);
        }

        public async Task<IActionResult> Update(int SSIN, [FromBody] UpdateWorkerDto dto)
        {
            Result result = await _workerRepository.HandleAsync(new UpdateWorkerCommand(
                SSIN,
                dto.FirstName,
                dto.LastName,
                dto.Gender,
                dto.BirthDate,
                dto.Email,
                dto.PhoneNumber,
                dto.Occupation,
                dto.Street,
                dto.Number,
                dto.ZipCode,
                dto.Town,
                dto.Country), CancellationToken.None);

            return this.FromResult(result);
        }

        public async Task<IActionResult> Delete(int SSIN)
        {
            Result result = await _workerRepository.HandleAsync(new DeleteWorkerCommand(SSIN), CancellationToken.None);
            return this.FromResult(result);
        }
    }
}
