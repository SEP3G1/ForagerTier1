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
        public string Cvr { get; set; } 
        public double TrustScore { get; set; }
        public int NumberOfVotes { get; set; }
        public string Name { get; set; } 
        public string Address { get; set; } 
        public string PostCode { get; set; } 
        public string Logo { get; set; }

        public List<Employee> Employees { get; set; }
        public string ConnectionAddress { get; set; }

    }
}
