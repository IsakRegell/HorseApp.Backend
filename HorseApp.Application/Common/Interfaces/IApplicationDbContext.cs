using HorseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HorseApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Post> Posts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
