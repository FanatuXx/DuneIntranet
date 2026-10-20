using PFF.Domain.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos
{
    public class CreateWorkerDto
    {
        [Required]
        public int SSIN { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; } = default!;

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Gender { get; set; } = default!;

        [Required] 
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Occupation { get; set; } = default!;

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Street { get; set; } = default!;

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Number { get; set; } = default!;

        [Required] 
        public int ZipCode { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Town { get; set; } = default!;

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Country { get; set; } = default!;
    }
}
