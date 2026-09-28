using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFF.Domain.Model.Entities
{
    public class Consultation
    {
        public int Id { get; set; }
        public string Motive { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int PathologyId { get; set; } 
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual IList<Worker> Workers { get; set; } = new List<Worker>(); //PAS SUR SI LISTE OU PAS
        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public virtual IList<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();

        public virtual IEnumerable<Pathology> Pathologies { get; set; } = new List<Pathology>();


    }
}
