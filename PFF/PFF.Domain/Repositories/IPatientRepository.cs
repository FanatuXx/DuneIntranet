using PFF.Domain.Commands.Patient;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Patients;
using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Repositories
{
    public interface IPatientRepository :
        ICommandAsyncHandler<CreatePatientCommand>,
        ICommandAsyncHandler<UpdatePatientCommand>,
        ICommandAsyncHandler<DeletePatientCommand>,
        IQueryAsyncHandler<GetPatientsQuery, IEnumerable<Patient>>,
        IQueryAsyncHandler<GetPatientDetailsQuery, Patient>
    {
    }
}
