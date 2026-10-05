using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class PatientAddress
    {
        public int Id { get; set; }
        public string? Street { get; set; } = default!;
        public int? Number { get; set; }
        public int ZipCode { get; set; }
        public string? Town { get; set; } = default!;
        public string? Country { get; set; } = default!;
        public virtual IList<Patient> Patients { get; set; } = new List<Patient>();
    }
}
