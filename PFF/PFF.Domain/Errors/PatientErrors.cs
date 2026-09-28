using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Errors
{
    public static class PatientErrors
    {
        public static Error PatientException => Error.Create("Patient.Exception", "Une exception est survenue.");
        public static Error PatientNotFound => Error.Create("Patient.NotFound", "Le patient n'a pas été trouvé.");
    }
}
