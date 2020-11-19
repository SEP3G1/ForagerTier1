using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Cvr { get; set; } // Udfyld
        public double TrustScore { get; set; }
        public int NumberOfVotes { get; set; }
        public string Name { get; set; } // Udfyld
        public string Address { get; set; } // Udfyld
        public string PostCode { get; set; } // Udfyld
        public string Logo { get; set; }

        public List<Employee> Employees { get; set; }
        public string ConnectionAddress { get; set; } //Udfyld

    }
}
