namespace PFF.Domain.Model.ResponseDtos
{
    public class WorkerSummaryDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Occupation { get; set; } = default!;
    }
}
