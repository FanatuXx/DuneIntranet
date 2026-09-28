using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HowToTake { get; set; }
        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public virtual IList<ChronicTreatment> ChronicTreatments { get; set; } = new List<ChronicTreatment>();
    }
}


