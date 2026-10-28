using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string HowToTake { get; set; } = default!;
        public virtual IList<Prescription>? Prescriptions { get; set; }
        public virtual IList<ChronicTreatment>? ChronicTreatments { get; set; }
    }
}


