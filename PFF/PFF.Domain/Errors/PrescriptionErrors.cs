using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Errors
{
    public static class PrescriptionErrors
    {
        public static Error PrescriptionException => Error.Create("Prescription.Exception", "Une exception est survenue.");
        public static Error PrescriptionNotFound => Error.Create("Prescription.NotFound", "La prescription n'a pas été trouvée.");
    }
}
