using MS.Domain.Authorization.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Domain.Authorization.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid Id { get; set; }
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
