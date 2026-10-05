using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos
{
    public class CreatePatientAddressDto
    {
        public string? Street { get; set; } = default;
        public int? Number { get; set; }
        [Required]
        public int ZipCode { get; set; }
        public string? Town { get; set; } = default;
        public string? Country { get; set; } = default;
    }
}
