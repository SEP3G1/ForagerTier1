using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
        User GetUser();
        User GetUser(int id);
    }
}
