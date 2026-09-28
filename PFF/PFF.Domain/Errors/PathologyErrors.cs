using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Errors
{
    public static class PathologyErrors
    {
        public static Error PathologyException => Error.Create("Pathology.Exception", "Une exception est survenue.");
        public static Error PathologyNotFound => Error.Create("Pathology.NotFound", "La pathologie n'a pas été trouvée.");
    }
}
