using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Errors
{
    public static class ConsultationErrors
    {
        public static Error ConsultationException => Error.Create("Consultation.Exception", "Une exception est survenue.");
        public static Error ConsultationNotFound => Error.Create("Consultation.NotFound", "La consultation n'a pas été trouvée.");
    }
}
