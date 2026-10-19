using PFF.Domain.Model.Entities;

namespace PFF.Api.Dtos
{
    public class CreateChronicTreatmentDto
    {

        public string Dosage { get; set; } = default!;
        public int PathologyId { get; set; }
        public int PatientId { get; set; }
        public List<int> MedicineIds { get; set; } = new();
    }
}
