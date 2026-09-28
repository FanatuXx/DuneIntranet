using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Errors
{
    public static class VitalSignErrors
    {
        public static Error VitalSignException => Error.Create("VitalSign.Exception", "Une exception est survenue.");
        public static Error VitalSignNotFound => Error.Create("VitalSign.NotFound", "Les paramètres vitaux n'ont pas été trouvés.");
    }
}
