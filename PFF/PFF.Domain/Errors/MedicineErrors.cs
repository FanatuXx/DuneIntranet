using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Errors
{
    public static class MedicineErrors
    {
        public static Error MedicineException => Error.Create("Medicine.Exception", "Une exception est survenue.");
        public static Error MedicineNotFound => Error.Create("Medicine.NotFound", "Le médicament n'a pas été trouvé.");
    }
}
