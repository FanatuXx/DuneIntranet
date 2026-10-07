using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos
{
    public class CreatePatientAddressDto
    {
        public string? Street { get; set; }
        public string? Number { get; set; }
        [Required]
        public string ZipCode { get; set; } = default!;
        public string? Town { get; set; } 
        public string? Country { get; set; } 
    }
}
