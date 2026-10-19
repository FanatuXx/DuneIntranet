using PFF.Domain.Commands.Medicines;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Medicines;
using PFF.Tools.CommandQuerySeparation;


namespace PFF.Domain.Repositories
{
    public interface IMedicineRepository :
        IQueryAsyncHandler<GetMedicinesQuery, IEnumerable<Medicine>>,
        IQueryAsyncHandler<GetMedicineDetailsQuery, Medicine>,
        ICommandAsyncHandler<CreateMedicineCommand>,
        ICommandAsyncHandler<UpdateMedicineCommand>,
        ICommandAsyncHandler<DeleteMedicineCommand>
    {
    }
}
