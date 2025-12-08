using AutoMapper;
using HorseApp.Application.Common.DTOs;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Users;
using MediatR;

namespace HorseApp.Application.Users.Queries
{
    public sealed class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, PaginationResponseDto<UserListItemDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<UserListItemDto>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            // 1. Hämta sida + totalcount från repo
            var (users, totalCount) = await _userRepository.GetUsersAsync(
                request.Page,
                request.PageSize,
                cancellationToken);

            // 2. Mappa entities -> DTOs
            var userDtos = _mapper.Map<List<UserListItemDto>>(users);

            // 3. Bygg pagination-response
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
