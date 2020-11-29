using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public interface ISocketService
    {
        SearchQuery Search(string message);
        SearchQuery LazyFilterSearch(string message, string filter, int sequenceNumber);
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
        Dictionary<string, string> GetListingNamesAndCover();
        List<string> GetListingPostCodes();
        User GetUser(int id);
        Company GetCompanyFromUserId(int id);
        Company GetCompany(string id);
        void SendMessage(string Message, int SendToUserId, int SendFromCompanyId);
        string UpdateCompany(Company company);

    }
}