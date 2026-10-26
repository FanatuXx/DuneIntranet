using PFF.Domain.Model.Entities;

namespace PFF.Domain.Model.ResponseDtos
{
    public class PrescriptionWithMedicineDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string TakeFrequency { get; set; } = default!;
        public int WorkerId { get; set; }
        public virtual IList<MedicineSummaryDto> Medicines { get; set; } = new List<MedicineSummaryDto>();
    }
}
