
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForagerTier1.Models;

namespace ForagerTier1.Persistance
{
    public class CompanyService : ICompanyService
    {
        public Company Company { get; set; }
        public string CreateCompany(Company newCompany)
        {
            SocketService socketService = new SocketService();
            string id = socketService.AddCompany(newCompany);
            return id;
        }

        public Company GetCompany(string id)
        {
            SocketService socketService = new SocketService();
            return socketService.GetCompany(id);
        }
    }
}
