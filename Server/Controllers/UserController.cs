using Microsoft.AspNetCore.Http;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Users.Commands.CreateUser;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        public UserController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
    }
}
