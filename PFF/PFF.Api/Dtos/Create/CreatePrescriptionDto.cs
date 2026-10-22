using PFF.Domain.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Create
{
    public class CreatePrescriptionDto
    {
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string TakeFrequency { get; set; } = default!;

        [Required]
        public int PrescriberId { get; set; }

        [Required]
        public virtual IList<int> MedicinesIds { get; set; } = new List<int>();
    }
}
