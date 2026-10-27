using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.ResponseDtos
{
    public class PatientAddressSummaryDto
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string ZipCode { get; set; } = default!;
        public string? Town { get; set; }
        public string? Country { get; set; }
    }
}
