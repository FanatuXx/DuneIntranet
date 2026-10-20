using PFF.Domain.Commands.Consultations;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Consultations;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IConsultationRepository :
        IQueryAsyncHandler<GetConsultationsQuery, IEnumerable<Consultation>>,
        IQueryAsyncHandler<GetConsultationDetailsQuery, Consultation>,
        ICommandAsyncHandler<CreateConsultationCommand>,
        ICommandAsyncHandler<UpdateConsultationCommand>,
        ICommandAsyncHandler<DeleteConsultationCommand>
    {
    }
}
