using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos;
using PFF.Domain.Commands.PatientAddress;
using PFF.Domain.Model.Entities;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System.Globalization;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/patients/{patientId:int}/address")]
    public class PatientAddressController : ControllerBase
    {
        private readonly IPatientAddressRepository _patientAddressRepository;

        public PatientAddressController(IPatientAddressRepository patientAddressRepository)
        {
            _patientAddressRepository = patientAddressRepository;
        }

        [HttpPost]
        public IActionResult Post(int patientId, [FromBody] CreatePatientAddressDto dto)
        {
            Result result = _patientAddressRepository.Handle(new CreatePatientAddressCommand(
                    patientId,
                    dto.Street,
                    dto.Number,
                    dto.ZipCode,
                    dto.Town,
                    dto.Country
                ));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/patientAddress", null);
        }
    }
}
