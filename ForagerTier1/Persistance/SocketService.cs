using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;

namespace ForagerTier1.Models
{
    public class SocketService : ISocketService
    {
        private static string IP = "192.168.87.168";
        private static int PORT = 4343;
        private static Socket clientSocket;
        public event EventHandler SomethingHappened;
        IPEndPoint serverAddress;
        private JsonSerializerOptions options;

        public SocketService()
        {
            options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverAddress);
        }
        public string SendReceive(string message)
        {
            // Sending
            int toSendLen = System.Text.Encoding.ASCII.GetByteCount(message);
            byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(message);
            byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
            clientSocket.Send(toSendLenBytes);
            clientSocket.Send(toSendBytes);

            // Receiving
            byte[] rcvLenBytes = new byte[4];
            clientSocket.Receive(rcvLenBytes);
            int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
            byte[] rcvBytes = new byte[rcvLen];
            clientSocket.Receive(rcvBytes);

            //Converts recieved bits to string
            return System.Text.Encoding.ASCII.GetString(rcvBytes);
        }

        public SearchQuery Search(string message)
        { 
            string[] r = { "search", message };
   
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            //Deserializing received query
            return JsonSerializer.Deserialize<SearchQuery>(rcv, options); ;
        }

        public SearchQuery LazyFilterSearch(string message, string filter, int sequenceNumber)
        {
            string[] r = { "lazyFilterSearch", message, filter, sequenceNumber+"" };
         
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            //Deserializing received query
            return JsonSerializer.Deserialize<SearchQuery>(rcv, options);
        }

        public int GetNumberOfResults(string message)
        {
            string[] r = { "getNumberOfResults", message };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            //Deserializing received query
            return JsonSerializer.Deserialize<int>(rcv, options); ;
        }

        public User Login(string username, string password)
        {
            string[] u = { username, password };
            string[] r = { "login", JsonSerializer.Serialize(u) };
           
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            //Deserializing received query
            return JsonSerializer.Deserialize<User>(rcv, options);
        }

        public string CreateListing(Listing listing)
        {
            string[] r = {"createlisting", JsonSerializer.Serialize(listing)};
           
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return rcv;
        }
        
        public int GetUnreadMessages()
        {
            string[] r = { "unread", "unread" };
         
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return int.Parse(rcv);
        }

        public string AddCompany(Company company)
        {
            string[] r = { "createcompany", JsonSerializer.Serialize(company) };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return rcv;
        }

      

        public string UploadImageTest(IList<IBrowserFile> imgs)
        {
            string[] r = { "uploadImage", JsonSerializer.Serialize(imgs) };
     
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return rcv;
        }

        public Listing GetListing(string id)
        {

            string[] r = { "getlisting", id };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<Listing>(rcv, options);
        }

        public Company GetCompany(string id)
        {

            string[] r = { "getcompany", id };
       
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<Company>(rcv, options);
        }

        public Company GetCompanyFromUserId(int id)
        {

            string[] r = { "getcompanyFromUserId", id + ""};
         
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<Company>(rcv, options); 
        }
        public List<Product> GetProducts()
        {

            string[] r = { "getproducts", "" };
     
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<List<Product>>(rcv, options);
        }

        public List<string> GetProductCategories()
        {

            string[] r = { "getproductcategories", "" };
   
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<List<string>>(rcv, options); ;
        }

        public List<Message> SendMessage(string Message, int SendToUserId, int SendFromCompanyId, int ListingId)
        {
            string[] a = {Message, SendToUserId + "", SendFromCompanyId + "", ListingId + ""};
            string[] r = { "sendMessage", JsonSerializer.Serialize(a) };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<List<Message>>(rcv, options); ;
        }

        public List<Message> Respond(string Message, Message m)
        {
            m.message = Message;
            m.timestamp = DateTime.Now.Ticks + "";
            Company c = m.fromCompany;
            m.fromCompany = m.toCompany;
            m.toCompany = c;
            string[] r = { "sendMessageWM", JsonSerializer.Serialize(m) };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<List<Message>>(rcv, options);
        }
        public User GetUser(int id)
        {

            string[] r = { "getUser", id + ""};

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<User>(rcv, options);
        }

        public string UpdateListing(Listing listing)
        {

            string[] r = { "updatelisting", JsonSerializer.Serialize(listing) };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return rcv;
        }

        public string UpdateCompany(Company company)
        {
            string[] r = { "updatecompany", JsonSerializer.Serialize(company) };
 
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return rcv;
        }

        public List<Message> GetConversation(int ListingId)
        {
            string[] r = { "getConversation", ListingId + "" };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<List<Message>>(rcv, options); ;
        }

        public Dictionary<string, string> GetListingNamesAndCover()
        {
            string[] r = { "getListingNamesAndCovers", null };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            //Deserializing received query
            return JsonSerializer.Deserialize<Dictionary<string, string>>(rcv, options); ;
        }

        public List<string> GetListingPostCodes()
        {
            string[] r = { "getListingPostCodes", null };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            //Deserializing received query
            return JsonSerializer.Deserialize<List<string>>(rcv, options); ;
        }

        public string ReportListing(Report report)
        {
            string[] r = { "reportlisting", JsonSerializer.Serialize(report) };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return rcv;
        }

        public List<Report> GetAllReports()
        {
            string[] r = { "getallreports", "" };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            //SKAL være Newtonsoft.Json ellers deserializer den til et array med 0
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Report>>(rcv); ;
        }

        public void DeleteCompanyWish(int id)
        {
            string[] r = { "deletecompanywish", id + "" };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
        }

        public void DeleteCompany(int id)
        {
            string[] r = { "deletecompany", id + "" };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
        }

        public List<Company> GetAllCompaniesToDelete()
        {
            string[] r = { "getcompaniestodelete", "" };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Company>>(rcv); ;
        }
        public string AddUser(User user)
        {
            string[] r = { "createuser", JsonSerializer.Serialize(user) };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));
            return rcv;
        }

        public List<Listing> GetListingsFromCompany(int id)
        {
            string[] r = { "getListingsFromCompany", id + "" };

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(JsonSerializer.Serialize(r));

            return JsonSerializer.Deserialize<List<Listing>>(rcv, options);
        }
    }
}