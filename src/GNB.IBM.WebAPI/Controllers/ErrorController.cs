using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

            return Problem(
                instance: context.Path,
                title: context.Error.Message
                //detail: context.Error.StackTrace
            );
        }
    }
}
