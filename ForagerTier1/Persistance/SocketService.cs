﻿using Microsoft.AspNetCore.Components.Forms;
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
        private static string IP = "192.168.10.101";
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

            //Makes json deserializor case insensitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializing received query
            SearchQuery sq = JsonSerializer.Deserialize<SearchQuery>(rcv, options);

            return sq;
        }

        public SearchQuery LazyFilterSearch(string message, string filter, int sequenceNumber)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }
            
            string[] r = { "lazyFilterSearch", message, filter, sequenceNumber+"" };
            message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case insensitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializing received query
            SearchQuery sq = JsonSerializer.Deserialize<SearchQuery>(rcv, options);

            return sq;
        }

        public int GetNumberOfResults(string message)
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getNumberOfResults", message };
            message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case insensitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializing received query
            int numberOfResults = JsonSerializer.Deserialize<int>(rcv, options);

            return numberOfResults;
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

            //Makes json deserializor case insensitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializing received query
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

            //Makes json deserializor case insensitive
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

            //Makes json deserializor case insensitive
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

            //Makes json deserializor case insensitive
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
            //Makes json deserializor case insensitive
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
            //Makes json deserializor case insensitive
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

            //Makes json deserializor case insensitive
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

        public Dictionary<string, string> GetListingNamesAndCover()
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getListingNamesAndCovers", null };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case insensitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializing received query
            Dictionary<string, string> listingNamesAndCovers = JsonSerializer.Deserialize<Dictionary<string, string>>(rcv, options);
            return listingNamesAndCovers;
        }

        public List<string> GetListingPostCodes()
        {
            if (clientSocket == null)
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IP), PORT);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);
            }

            string[] r = { "getListingPostCodes", null };
            string message = JsonSerializer.Serialize(r);

            //Sends message to connected Rest web API and gets a response in json
            string rcv = SendReceive(message);

            //Makes json deserializor case insensitive
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());

            //Deserializing received query
            List<string> postCodes = JsonSerializer.Deserialize<List<string>>(rcv, options);
            return postCodes;
        }
    }
}