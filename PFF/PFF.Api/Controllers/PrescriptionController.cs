using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Prescriptions;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System.Globalization;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionController (IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _prescriptionRepository.HandleAsync(new GetPrescriptionsQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _prescriptionRepository.HandleAsync(new GetPrescriptionDetailsQuery(id));
            return this.FromResult(result);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
        //{
        //    Result result = await _prescriptionRepository.HandleAsync(new CreatePrescriptionCommand(
        //        dto.StartDate,
        //        dto.EndDate,
        //        dto.TakeFrequency,
        //        dto.ConsultationId,
        //        dto.PatientId,
        //        dto.WorkerId,
        //        dto.Medicines), CancellationToken.None);

        //    if (result.IsFailure)
        //        return BadRequest(result.Error);

        //    return Created($"https://localhost:7050/api/prescription", null);
        //}

        //[HttpPatch("{id}")]
        //[HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdatePrescriptionDto dto)
        //{
        //    Result result = await _prescriptionRepository.HandleAsync(new UpdatePrescriptionCommand(
        //        id,
        //        dto.StartDate,
        //        dto.EndDate,
        //        dto.TakeFrequency,
        //        dto.ConsultationId,
        //        dto.PatientId,
        //        dto.WorkerId,
        //        dto.Medicines), CancellationToken.None);

        //    return this.FromResult(result);
        //}

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _prescriptionRepository.HandleAsync(new DeletePrescriptionCommand(id), CancellationToken.None);
            return this.FromResult(result);
        }
    }
}
