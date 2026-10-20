using Microsoft.AspNetCore.Mvc;
using PFF.Domain.Repositories;
using PFF.Domain.Queries.Medicines;
using PFF.Domain.Commands.Medicines;
using PFF.Api.Infrastructure;
using PFF.Tools.Results;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;

namespace PFF.Api.Controllers
{
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicineRepository.HandleAsync(new GetMedicinesQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _medicineRepository.HandleAsync(new GetMedicineDetailsQuery(id));
            return this.FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicineDto dto)
        {
            Result result = await _medicineRepository.HandleAsync(new CreateMedicineCommand(
                dto.Name,
                dto.HowToTake), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result);

            return Created("https://localhost:7050/api/medicine", null);
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicineDto dto)
        {
            Result result = await _medicineRepository.HandleAsync(new UpdateMedicineCommand(
                id, 
                dto.Name, 
                dto.HowToTake), CancellationToken.None);

            return this.FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _medicineRepository.HandleAsync(new DeleteMedicineCommand(id), CancellationToken.None);
            return this.FromResult(result);
        }
    }
}
