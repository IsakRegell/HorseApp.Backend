using MediatR;

namespace HorseApp.Application.Posts.Commands
{
    public sealed record DeletePostCommand(Guid Id) : IRequest<Unit>;
}
