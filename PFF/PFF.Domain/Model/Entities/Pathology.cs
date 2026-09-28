using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Pathology
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public virtual IList<Consultation> Consultations { get; set; } = new List<Consultation>();
        public virtual IList<ChronicTreatment> ChronicTreatments { get; set; } = new List<ChronicTreatment>();

    }
}
