using HorseApp.Application.Common.DTOs;
using HorseApp.Application.DTOs.Users;
using HorseApp.Application.Users.Commands;
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
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUserProfile(Guid id, [FromBody] UpdateUserProfileDto dto, CancellationToken ct)
        {
            // 1. Bygg command-objektet
            var command = new UpdateUserProfileCommand(id, dto);

            // 2. Skicka commandet via MediatR
            var result = await _mediator.Send(command);

            // 3. Returnera 200 OK med uppdaterad användare
            return Ok(result);
        }


        [HttpGet("get-all-users")]
        public async Task<ActionResult<PaginationResponseDto<UserListItemDto>>> GetUsers(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            // 1. Bygg query-objektet
            var query = new GetAllUsersQuery(page, pageSize);

            // 2. Skicka queryn via MediatR
            var result = await _mediator.Send(query);

            // 3. Returnera 200 OK med pagination-response
            return Ok(result);
        }


    }
}
