using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Domain.Patients.Entities
{
    public class ContactInformation
    {
        public Guid PatientId { get; set; }
        public string CellPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
