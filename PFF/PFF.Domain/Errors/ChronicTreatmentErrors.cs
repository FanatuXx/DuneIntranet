using System;
using System.Collections.Generic;
using System.Text;
using PFF.Tools.Results;

namespace PFF.Domain.Errors
{
    public static class ChronicTreatmentErrors
    {
        public static Error ChronicTreatmentException => Error.Create("ChronicTreatment.Exception", "Une exception est survenue.");
        public static Error ChronicTreatmentNotFound => Error.Create("ChronicTreatment.NotFound", "Le traitement chronique n'a pas été trouvé.");
    }
}
