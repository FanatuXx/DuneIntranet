using Microsoft.AspNetCore.Mvc;
using PFF.Domain.Repositories;

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
    }
}
