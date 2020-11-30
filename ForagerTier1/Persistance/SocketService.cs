using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public class SocketService : ISocketService
    {
        private static string IP = "192.168.1.69";
        private static int PORT = 4343;
        private static Socket clientSocket;

        public SocketService()
        {
        }

        public SearchQuery Search(string message)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }
            
            string[] r = { "search", message };
            message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializeing recieved query
            SearchQuery sq = JsonSerializer.Deserialize<SearchQuery>(rcv, options);

            return sq;
        }

        public User Login(string username, string password)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }
            string[] u = { username, password };
            string[] r = { "login", JsonSerializer.Serialize(u) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializeing recieved query
            User sq = JsonSerializer.Deserialize<User>(rcv, options);

            return sq;
        }

        public string CreateListing(Listing listing)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = {"createlisting", JsonSerializer.Serialize(listing)};
            string message = JsonSerializer.Serialize(r);
            
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public string AddCompany(Company company)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "createcompany", JsonSerializer.Serialize(company) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public string UploadImageTest(IList<IBrowserFile> imgs)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "uploadImage", JsonSerializer.Serialize(imgs) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
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
        public void Send(string message)
        {
            // Sending
            int toSendLen = System.Text.Encoding.ASCII.GetByteCount(message);
            byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(message);
            byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
            clientSocket.Send(toSendLenBytes);
            clientSocket.Send(toSendBytes);
        }
        public Listing GetListing(string id)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getlisting", id };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            Listing listing = JsonSerializer.Deserialize<Listing>(rcv, options);
            return listing;
        }

        public Company GetCompany(string id)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getcompany", id };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            Company company = JsonSerializer.Deserialize<Company>(rcv, options);
            return company;
        }

        public Company GetCompanyFromUserId(int id)
        {

            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getcompanyFromUserId", id + ""};
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            Company company = JsonSerializer.Deserialize<Company>(rcv, options);
            return company;
        }
        public List<Product> GetProducts()
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getproducts", "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            List<Product> listing = JsonSerializer.Deserialize<List<Product>>(rcv, options);
            return listing;
        }

        public List<string> GetProductCategories()
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getproductcategories", "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            List<string> listing = JsonSerializer.Deserialize<List<string>>(rcv,options);
            return listing;
        }

        public void SendMessage(string Message, int SendToUserId, int SendFromCompanyId)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }
            string[] a = {Message, SendToUserId + "", SendFromCompanyId + ""};
            string[] r = { "sendMessage", JsonSerializer.Serialize(a) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API
            Send(message);
        }

        public User GetUser(int id)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getUser", id + ""};
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            User u= JsonSerializer.Deserialize<User>(rcv, options);
            return u;
        }

        public string UpdateListing(Listing listing)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "updatelisting", JsonSerializer.Serialize(listing) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public string UpdateCompany(Company company)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "updatecompany", JsonSerializer.Serialize(company) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public string ReportListing(Report report)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "reportlisting", JsonSerializer.Serialize(report) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public List<Report> GetAllReports()
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getallreports", "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //SKAL være Newtonsoft.Json ellers deserializer den til et array med 0
            List<Report> reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Report>>(rcv);
            return reports;
        }

        public void DeleteListing(int ListingId)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "deletelisting", ListingId + "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
        }
        public void DeleteCompanyWish(int id)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "deletecompanywish", id + "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            Console.WriteLine(rcv);
        }

        public void DeleteCompany(int id)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "deletecompany", id + "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            Console.WriteLine(rcv);
        }

        public List<Company> GetAllCompaniesToDelete()
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getcompaniestodelete", "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            List<Company> companies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Company>>(rcv);
            return companies;
        }
    }
}