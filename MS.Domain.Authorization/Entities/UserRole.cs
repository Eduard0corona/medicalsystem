using MS.Domain.Authorization.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Domain.Authorization.Entities
{
    public class UserRole : IEntity
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
