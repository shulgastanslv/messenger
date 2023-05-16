using Application.Users.Commands.AuthenticateUser;
using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ApiControllerBase
    {
        public RegistrationController(ISender mediator) : base(mediator){}

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
    }
}
