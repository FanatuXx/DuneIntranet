using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Worker
    {
        public int SSIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int ZipCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public virtual IList<Consultation> Consultations { get; set; } = new List<Consultation>();
        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
