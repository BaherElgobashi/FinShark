using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Portifolio
    {
        // Foreign key For AppUser.
        public string AppUserId { get; set; }

        // Navigational Property for AppUser.
        public AppUser AppUser { get; set; }

        // Foreign key For Stock.
        public int StockId { get; set; }

        // Navigational Property for AppUser.
        public Stock Stock { get; set; }

    }
}