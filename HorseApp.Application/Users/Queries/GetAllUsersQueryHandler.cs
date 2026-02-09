using AutoMapper;
using HorseApp.Application.Common.DTOs;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Application.Users.Queries
{
    public sealed class GetAllUsersQueryHandler
        : IRequestHandler<GetAllUsersQuery, PaginationResponseDto<UserListItemDto>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(
            IApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<UserListItemDto>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var baseQuery = _db.Users.AsNoTracking();

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            var users = await baseQuery
                .OrderByDescending(x => x.CreatedAtUtc)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var userDtos = _mapper.Map<List<UserListItemDto>>(users);

            var response = new PaginationResponseDto<UserListItemDto>
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                Items = userDtos
            };

            return response;
        }
    }
}
