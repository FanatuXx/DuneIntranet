using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Queries.VitalSigns;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/vitalsigns")]
    public class VitalSignController : Controller
    {
        private readonly IVitalSignRepository _vitalSignRepository;

        public VitalSignController(IVitalSignRepository vitalSignRepository)
        {
            _vitalSignRepository = vitalSignRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _vitalSignRepository.HandleAsync(new GetVitalSignsQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _vitalSignRepository.HandleAsync(new GetVitalSignDetailsQuery(id));
            return this.FromResult(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateVitalSignDto dto)
        //{
        //    Result result = await _vitalSignRepository.HandleAsync(new CreateVitalSignCommand(
        //        dto.HeartRate,
        //        dto.BloodPressure,
        //        dto.Temperature,
        //        dto.OxygenSaturation,
        //        dto.Height,
        //        dto.Weight,
        //        dto.BloodSugar,
        //        dto.Date,
        //        dto.ConsultationId), CancellationToken.None);

        //    if (result.IsFailure)
        //        return BadRequest(result.Error);

        //    return Created($"https://localhost:7050/api/vitalsign", null);
        //}

        //[HttpPatch("{id}")]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateVitalSignDto dto)
        //{
        //    Result result = await _vitalSignRepository.HandleAsync(new UpdateVitalSignCommand(
        //        id,
        //        dto.HeartRate,
        //        dto.BloodPressure,
        //        dto.Temperature,
        //        dto.OxygenSaturation,
        //        dto.Height,
        //        dto.Weight,
        //        dto.BloodSugar,
        //        dto.Date,
        //        dto.ConsultationId), CancellationToken.None); 
            
        //    return this.FromResult(result);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _vitalSignRepository.HandleAsync(new DeleteVitalSignCommand(id), CancellationToken.None);
            return this.FromResult(result);
        }

    }
}
