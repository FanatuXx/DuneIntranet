using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.Pathologies;
using PFF.Domain.Queries.Pathologies;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PathologyController : ControllerBase
    {
        private readonly IPathologyRepository _pathologyRepository;

        public PathologyController(IPathologyRepository pathologyRepository)
        {
            _pathologyRepository = pathologyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _pathologyRepository.HandleAsync(new GetPathologiesQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _pathologyRepository.HandleAsync(new GetPathologyDetailsQuery(id));
            return this.FromResult(result);
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreatePathologyDto dto)
        {
            Result result = await _pathologyRepository.HandleAsync(new CreatePathologyCommand(dto.Name), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/pathology", null);
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePathologyDto dto)
        {
            Result result = await _pathologyRepository.HandleAsync(new UpdateChronicTreatmentCommand(id, dto.Name), CancellationToken.None);
            return this.FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            Result result = await _pathologyRepository.HandleAsync(new DeleteChronicTreatmentCommand(id), CancellationToken.None);
            return this.FromResult(result);

        }

    }
}
