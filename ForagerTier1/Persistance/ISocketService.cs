﻿using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public interface ISocketService
    {
        SearchQuery Search(string message);
        string SendReceive(string message);
        string UploadImageTest(IList<IBrowserFile> imgs);
        User Login(string username, string password);
        string CreateListing(Listing listing);
        string UpdateListing(Listing listing);
        Listing GetListing(string id);
        List<Product> GetProducts();
        List<string> GetProductCategories();
        User GetUser(int id);
        Company GetCompanyFromUserId(int id);
        Company GetCompany(string id);
        void SendMessage(string Message, int SendToUserId, int SendFromCompanyId);
    }
}