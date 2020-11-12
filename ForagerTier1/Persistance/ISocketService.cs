using Microsoft.AspNetCore.Components.Forms;
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
        Listing GetListing(string id);
        List<Product> GetProducts();
        List<string> GetProductCategories();
    }
}