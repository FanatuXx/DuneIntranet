using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos
{
    public class UpdateWorkerDto
    {
        [StringLength(50, MinimumLength = 1)]
        public string? FirstName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string? LastName { get; set; }

        [StringLength(30, MinimumLength = 1)]
        public string? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Email { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string? PhoneNumber { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string? Occupation { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Street { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string? Number { get; set; }

        public int? ZipCode { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string? Town { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string? Country { get; set; }
    }
}
