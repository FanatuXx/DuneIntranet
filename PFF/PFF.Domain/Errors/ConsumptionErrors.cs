using System;
using System.Collections.Generic;
using System.Text;
using PFF.Tools.Results;

namespace PFF.Domain.Errors
{
    public static class ConsumptionErrors
    {
        public static Error ConsumptionException => Error.Create("Consumption.Exception", "Une exception est survenue.");
        public static Error ConsumptionNotFound => Error.Create("Consumption.NotFound", "Les informations relatives à la consommation n'ont pas été trouvées.");
    }
}
