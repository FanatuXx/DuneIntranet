using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Consumption
    {
        public int Id { get; set; }
        public string DrugType { get; set; } = default!;
        public string Frequency { get; set; } = default!;
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;
    }
}
