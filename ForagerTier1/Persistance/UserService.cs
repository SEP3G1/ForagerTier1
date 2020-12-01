using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public class UserService : IUserService
    {
        public User User { get; set; }
        public ISocketService socketService;

        public string CreateUser(User newUser)
        {
            if (socketService == null)
                socketService = new SocketService();
            string id = socketService.AddUser(newUser);
            return id;
        }
        public User GetUser()
        {
            return User;
        }

        public User GetUser(int id)
        {
            SocketService socketService = new SocketService();
            User first = socketService.GetUser(id);
            return first;
        }

        public User ValidateUser(string userName, string password)
        {
            SocketService socketService = new SocketService();
            User first = socketService.Login(userName,password);
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }
            User = first;

            return first;
        }

        
    }
}
