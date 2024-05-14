using Microsoft.EntityFrameworkCore;
using MS.Domain.Authorization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface IAuthorizationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<Token> Tokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
