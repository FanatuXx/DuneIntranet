using Microsoft.AspNetCore.Mvc;
using PFF.Tools.Results;

namespace PFF.Api.Infrastructure
{
    public static class ControllerBaseExtensions
    {
        extension(ControllerBase controller)
        {
            public IActionResult FromResult(Result result)
            {
                if (result.IsFailure)
                    return controller.BadRequest(result.Error);

                return controller.NoContent();
            }

            public IActionResult FromResult<T>(Result<T> result)
            {
                if (result.IsFailure)
                    return controller.BadRequest(result.Error);

                return controller.Ok(result.Data);
            }

        }
    }
}

