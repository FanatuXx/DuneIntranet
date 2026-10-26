namespace PFF.Domain.Model.ResponseDtos
{
    public class MedicineSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string HowToTake { get; set; } = default!;
    }
}
