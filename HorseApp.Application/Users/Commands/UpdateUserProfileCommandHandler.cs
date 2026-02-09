using AutoMapper;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.DTOs.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorseApp.Application.Users.Commands
{
    public sealed class UpdateUserProfileCommandHandler
        : IRequestHandler<UpdateUserProfileCommand, UserResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _db;

        public UpdateUserProfileCommandHandler(IMapper mapper, IApplicationDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<UserResponseDto> Handle(
            UpdateUserProfileCommand request,
            CancellationToken ct)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, ct);

            if (user is null)
                throw new KeyNotFoundException($"Användare med id '{request.UserId}' hittades ej.");

            // 2. Mappa DTO → befintlig entity (uppdatera fälten)
            _mapper.Map(request.Payload, user);

            // 3. Sätt UpdatedAtUtc
            user.UpdatedAtUtc = DateTime.UtcNow;

            // 4. Spara ändringar
            await _db.SaveChangesAsync(ct);

            // 5. Mappa entity → response dto
            var response = _mapper.Map<UserResponseDto>(user);

            return response;
        }
    }
}
