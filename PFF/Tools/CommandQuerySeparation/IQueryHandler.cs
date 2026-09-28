using System;
using System.Collections.Generic;
using System.Text;
using PFF.Tools.Results;

namespace PFF.Tools.CommandQuerySeparation
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQueryDefinition<TResult>
    {
        Result<TResult> Handle(TQuery query);
    }

    public interface IQueryAsyncHandler<TQuery, TResult>
        where TQuery : IQueryDefinition<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query);
    }
}
