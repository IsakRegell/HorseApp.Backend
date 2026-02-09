using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Application.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDto?>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserResponseDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
                return null;

            var response = _mapper.Map<UserResponseDto>(user);

            return response;
        }
    }
}
