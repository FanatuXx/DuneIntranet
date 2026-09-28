using System;
using System.Collections.Generic;
using System.Text;
using PFF.Tools.Results;

namespace PFF.Domain.Errors
{
    public static class WorkerErrors
    {
        public static Error WorkerException => Error.Create("Worker.Exception", "Une exception est survenue.");
        public static Error WorkerNotFound => Error.Create("Worker.NotFound", "Le travailleur n'a pas été trouvé.");
    }
}
