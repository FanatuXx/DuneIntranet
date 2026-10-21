using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string TakeFrequency { get; set; } = default!;
        public int  ConsultationId { get; set; }
        public int PatientId { get; set; }
        public int WorkerId { get; set; }
        public virtual IList<Medicine> Medicines { get; set; } = new List<Medicine>();
        public virtual Worker Worker { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
        public virtual Consultation Consultation { get; set; } = null!;
    }
}

