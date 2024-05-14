using MS.Domain.Patients.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Domain.Patients.Entities
{
    public class MedicalInformation
    {
        public BloodType BloodType { get; set; }
        public string Allergies { get; set; } = string.Empty;
        public string ChronicDiseases { get; set; } = string.Empty;
        public string CurrentMedications { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;

    }
}
