using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        public UserController(ISender mediator) : base(mediator){}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());

            return result.Succeeded ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(string Id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(Id));

            return result.Succeeded ? Ok(result) : BadRequest(result.Errors);
        }
    }
}
