using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Prescriptions;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IPrescriptionRepository :
        IQueryAsyncHandler<GetPrescriptionsQuery, IEnumerable<Prescription>>,
        IQueryAsyncHandler<GetPrescriptionDetailsQuery, Prescription>,
        ICommandAsyncHandler<CreatePrescriptionCommand>,
        ICommandAsyncHandler<UpdatePrescriptionCommand>,
        ICommandAsyncHandler<DeletePrescriptionCommand>
    {
    }
}
