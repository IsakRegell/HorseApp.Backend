using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Users;
using HorseApp.Domain.Entities;
using MediatR;

namespace HorseApp.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var dto = request.Dto;

            // 1. Dublettkontroller (behåll som du hade)
            if (await _userRepository.UsernameExistsAsync(dto.Username, ct))
            {
                throw new InvalidOperationException("Användarnamnet är redan upptaget.");
            }

            if (await _userRepository.EmailExistsAsync(dto.Email, ct))
            {
                throw new InvalidOperationException("E-postadressen används redan.");
            }

            // 2. DTO -> Entity via AutoMapper
            var user = _mapper.Map<User>(dto);

            // 3. Sätt systemfält som inte kommer från klienten
            user.Id = Guid.NewGuid();
            user.IsBankIdVerified = false;
            user.VerifiedAtUtc = null;
            user.CreatedAtUtc = DateTime.UtcNow;
            user.UpdatedAtUtc = null;

            await _userRepository.AddUserAsync(user, ct);
            await _userRepository.SaveChangesAsync(ct);

            // 4. Entity -> Response-DTO via AutoMapper
            var response = _mapper.Map<UserResponseDto>(user);

            return response;
        }
    }
}
