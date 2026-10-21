using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.VitalSigns;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IVitalSignRepository :
        IQueryAsyncHandler<GetVitalSignsQuery, IEnumerable<VitalSign>>,
        IQueryAsyncHandler<GetVitalSignDetailsQuery, VitalSign>,
        ICommandAsyncHandler<CreateVitalSignCommand>,
        ICommandAsyncHandler<UpdateVitalSignCommand>,
        ICommandAsyncHandler<DeleteVitalSignCommand>
    {
    }
}
