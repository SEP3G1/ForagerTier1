using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public interface ISocketService
    {
        SearchQuery Search(string message);
        SearchQuery LazyFilterSearch(string message, string filter, int sequenceNumber);
        int GetNumberOfResults(string message);
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
        List<Message> GetConversation(int ListingId);
        Dictionary<string, string> GetListingNamesAndCover();
        List<string> GetListingPostCodes();
        User GetUser(int id);
        Company GetCompanyFromUserId(int id);
        Company GetCompany(string id);
        List<Message> SendMessage(string Message, int SendToUserId, int SendFromCompanyId, int ListingId);
        List<Message> Respond(string Message, Message m);
        string UpdateCompany(Company company);

        string ReportListing(Report report);

        List<Report> GetAllReports();

        void DeleteListing(int ListingId);
        void DeleteCompanyWish(int id);
        void DeleteCompany(int id);
        List<Company> GetAllCompaniesToDelete();
    }
}