using Application.Users.Commands.AuthenticateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ApiControllerBase
    {
        public AuthenticateController(ISender mediator) : base(mediator){}

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
    }
}
