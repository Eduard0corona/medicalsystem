using MS.Domain.Authorization.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Domain.Authorization.Entities
{
    public class Token : BaseEntity<Guid>
    {
        public string Value { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
    }
}
