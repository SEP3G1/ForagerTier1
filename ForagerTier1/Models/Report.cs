using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public int ListingId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string ReportType { get; set; }
        [Required]
        public string Time { get; set; }
    }
}
