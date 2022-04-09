using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GNB.IBM.WebAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (context == null)
            {
                return NotFound();
            }

            Log.Error(context.Error, $"URI: {context.Path} - Message: {context.Error.Message}");

            return Problem(
                instance: context.Path,
                title: context.Error.Message
                //detail: context.Error.StackTrace
            );
        }
    }
}
