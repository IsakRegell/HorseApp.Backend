using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Users;
using HorseApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var dto = request.Dto;

            // 1. Dublettkontroller (behåll som du hade)
            var usernameExists = await _db.Users.AnyAsync(x => x.Username == dto.Username, ct);
            if (usernameExists)
                throw new InvalidOperationException("Användarnamnet är redan upptaget.");

            var emailExists = await _db.Users.AnyAsync(x => x.Email == dto.Email, ct);
            if (emailExists)
                throw new InvalidOperationException("E-postadressen används redan.");

            // 2. DTO -> Entity via AutoMapper
            var user = _mapper.Map<User>(dto);

            // 3. Sätt systemfält som inte kommer från klienten
            user.Id = Guid.NewGuid();
            user.IsBankIdVerified = false;
            user.VerifiedAtUtc = null;
            user.CreatedAtUtc = DateTime.UtcNow;
            user.UpdatedAtUtc = null;

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);

            // 4. Entity -> Response-DTO via AutoMapper
            var response = _mapper.Map<UserResponseDto>(user);

            return response;
        }
    }
}
