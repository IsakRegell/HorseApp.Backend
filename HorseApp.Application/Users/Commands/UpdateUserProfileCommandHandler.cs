using AutoMapper;
using HorseApp.Application.Users.Commands;
using HorseApp.Application.Common.Interfaces; 
using MediatR;
using HorseApp.Application.DTOs.Users;

namespace HorseApp.Application.Users.Commands
{
    public sealed class UpdateUserProfileCommandHandler
        : IRequestHandler<UpdateUserProfileCommand, UserResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> Handle(
        UpdateUserProfileCommand request,
        CancellationToken ct)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId, ct);

            if (user is null)
            {
                throw new KeyNotFoundException($"Användare med id '{request.UserId}' hittades ej.");
            }

            // 2. Mappa DTO → befintlig entity (uppdatera fälten)
            _mapper.Map(request.Payload, user);

            // 3. Sätt UpdatedAtUtc
            user.UpdatedAtUtc = DateTime.UtcNow;

            // 4. Spara ändringar
            await _userRepository.SaveChangesAsync(ct);

            // 5. Mappa entity → response dto
            var response = _mapper.Map<UserResponseDto>(user);

            return response;

        }

    }
}
