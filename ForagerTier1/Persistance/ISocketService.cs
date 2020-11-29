using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public interface ISocketService
    {
        SearchQuery Search(string message);
        string SendReceive(string message);
        void Send(string message);
        string UploadImageTest(IList<IBrowserFile> imgs);
        User Login(string username, string password);
        string CreateListing(Listing listing);
        string UpdateListing(Listing listing);
        string AddCompany(Company newCompany);
        Listing GetListing(string id);
        List<Product> GetProducts();
        List<string> GetProductCategories();
        int GetUnreadMessages();
        int GetNotifications();
        List<Message> GetConversation(int ListingId);
        User GetUser(int id);
        Company GetCompanyFromUserId(int id);
        Company GetCompany(string id);
        List<Message> SendMessage(string Message, int SendToUserId, int SendFromCompanyId, int ListingId);
        List<Message> Respond(string Message, Message m);
        string UpdateCompany(Company company);
    }
}