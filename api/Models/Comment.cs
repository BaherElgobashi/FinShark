using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        // Primary Key.
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        // Foreign key to Stock.
        public int? StockId { get; set; }

        // Navigation property to Stock.
        public Stock? Stock { get; set; }

        // Foreign key for AppUser.
        public string AppUserId { get; set; }

        // Navigation property to AppUser.
        public AppUser AppUser { get; set; }


    }
}