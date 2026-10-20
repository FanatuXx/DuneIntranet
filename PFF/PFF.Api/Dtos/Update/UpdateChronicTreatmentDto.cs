namespace PFF.Api.Dtos.Update
{
    public class UpdateChronicTreatmentDto
    {
        public string? Dosage { get; set; } = default!;
        public int? PathologyId { get; set; }
        public int? PatientId { get; set; }
        public List<int>? MedicineIds { get; set; }
    }
}
