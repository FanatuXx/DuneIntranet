using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.ResponseDtos
{
    public class ConsumptionSummaryDto
    {
        public int Id { get; set; }
        public string DrugType { get; set; } = default!;
        public string Frequency { get; set; } = default!;
    }
}
