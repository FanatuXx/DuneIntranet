using Microsoft.AspNetCore.Mvc;
using PFF.Domain.Repositories;
using PFF.Api.Infrastructure;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries;
using PFF.Api.Dtos;
using PFF.Tools.Results;
using PFF.Domain.Commands;

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
        public IActionResult Get()
        {
            return this.FromResult(_pathologyRepository.Handle(new GetPathologiesQuery()));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePathologyDto dto)
        {
            Result result = _pathologyRepository.Handle(new CreatePathologyCommand(dto.Name));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/pathology", null);
        }

    }
}
