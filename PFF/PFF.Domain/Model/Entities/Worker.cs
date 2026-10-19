using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Worker
    {
        public string SSIN { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Occupation { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        public string Town { get; set; } = default!;
        public string Country { get; set; } = default!;
        public virtual IList<Consultation> Consultations { get; set; } = new List<Consultation>();
        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
