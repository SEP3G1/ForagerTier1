
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
        public ISocketService socketService;
        public string CreateCompany(Company newCompany)
        {
            if(socketService == null)
                socketService = new SocketService();
            string id = socketService.AddCompany(newCompany);
            return id;
        }

        public Company GetCompany(string id)
        {
            if (socketService == null)
                socketService = new SocketService();
            return socketService.GetCompany(id);
        }

        public Company GetCompanyFromUserId(int id)
        {
            if (socketService == null)
                socketService = new SocketService();
            return socketService.GetCompanyFromUserId(id);
        }

        public Company UpdateCompany(Company company)
        {
            if (socketService == null)
                socketService = new SocketService();
            socketService.UpdateCompany(company);
            return company;
        }
        public void DeleteCompanyWish(int id)
        {
            if (socketService == null)
                socketService = new SocketService();
            socketService.DeleteCompanyWish(id);
        }
    }
}
