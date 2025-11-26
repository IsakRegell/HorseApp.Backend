using HorseApp.Application.Users.Commands;
using HorseApp.Application.DTOs.Users;
using HorseApp.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorseApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id, CancellationToken ct)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query, ct);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }



        [HttpPost("create")]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            var command = new CreateUserCommand(dto);
            var result = await _mediator.Send(command, ct);
            return Ok(result);

            //return CreatedAtAction(nameof(CreateUser), new { id = result.Id }, result);
        }

    }
}
