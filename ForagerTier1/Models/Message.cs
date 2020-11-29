using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public class Message
    {
        public string message { get; set; }
        public Company fromCompany { get; set; }
        public Company toCompany { get; set; }
        public String timestamp { get; set; }
        public int ListingId { get; set; }
    }
}
