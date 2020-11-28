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
        private static Timer notificationTimer;
        private int notifications = 0;
        private int? Chash { get; set; }
        public event EventHandler SomethingHappened;

        public SocketService()
        {
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverAddress);
            //SetTimer();

        }

        public SearchQuery Search(string message)
        { 
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
            string[] r = {"createlisting", JsonSerializer.Serialize(listing)};
            string message = JsonSerializer.Serialize(r);
            
            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            notificationTimer = new Timer(1000);
            // Hook up the Elapsed event for the timer. 
            notificationTimer.Elapsed += OnTimedEvent;
            notificationTimer.AutoReset = true;
            notificationTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Chash == null)
                Chash = GetUnreadMessages();
            else
            {
                if (Chash != GetUnreadMessages())
                {
                    notifications++;
                    SomethingHappened?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        public int GetUnreadMessages()
        {
            string[] r = { "unread", "unread" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return int.Parse(rcv);
        }

        public string AddCompany(Company company)
        {
            string[] r = { "createcompany", JsonSerializer.Serialize(company) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public string UploadImageTest(IList<IBrowserFile> imgs)
        {
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
        public int GetNotifications()
        {
            return notifications;
        }
        public Listing GetListing(string id)
        {

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

        public List<Message> SendMessage(string Message, int SendToUserId, int SendFromCompanyId, int ListingId)
        {
            string[] a = {Message, SendToUserId + "", SendFromCompanyId + "", ListingId + ""};
            string[] r = { "sendMessage", JsonSerializer.Serialize(a) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            List<Message> Conversation = JsonSerializer.Deserialize<List<Message>>(rcv, options);
            return Conversation;
        }

        public User GetUser(int id)
        {

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

            string[] r = { "updatelisting", JsonSerializer.Serialize(listing) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public string UpdateCompany(Company company)
        {

            string[] r = { "updatecompany", JsonSerializer.Serialize(company) };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            return rcv;
        }

        public List<Message> GetConversation(int ListingId)
        {
            string[] r = { "getConversation", ListingId + "" };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);
            //Makes json deserializor case-insencitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            List<Message> Conversation = JsonSerializer.Deserialize<List<Message>>(rcv, options);
            return Conversation;
        }
    }
}