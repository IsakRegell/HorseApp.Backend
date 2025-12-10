using HorseApp.Application.DTOs.Posts;
using HorseApp.Application.Posts.Commands;
using HorseApp.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorseApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllPostsQuery(page, pageSize));
            return Ok(result);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            return Ok(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            var result = await _mediator.Send(new CreatePostCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostDto dto)
        {
            var result = await _mediator.Send(new UpdatePostCommand(id, dto));
            return Ok(result);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePostCommand(id));
            return NoContent();
        }
    }
}
