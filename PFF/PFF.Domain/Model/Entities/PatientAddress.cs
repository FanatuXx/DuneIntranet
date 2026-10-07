using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class PatientAddress
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string ZipCode { get; set; }
        public string? Town { get; set; } 
        public string? Country { get; set; }
        public virtual IList<Patient> Patients { get; set; } = new List<Patient>();
    }
}
