using MS.Domain.Authorization.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Domain.Authorization.Entities
{
    public class User : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; }
        public IReadOnlyList<Role>? Roles { get; set; }
        public IReadOnlyList<Token>? Tokens { get; set; }
    }
}
