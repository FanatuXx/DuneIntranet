using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class ChronicTreatment
    {
        public int Id { get; set; }
        public string Dosage { get; set; } = default!;
        public int PathologyId { get; set; }
        public int PatientId { get; set; }
        public virtual Pathology Pathology { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
        public virtual IList<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
