using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Errors
{
    public static class PatientAddressErrors
    {
        public static Error PatientAddressException => Error.Create("PatientAddress.Exception", "Une exception est survenue.");
        public static Error PatientAddressNotFound => Error.Create("PatientAddress.NotFound", "L'addresse du patient n'a pas été trouvée.");
    }
}
