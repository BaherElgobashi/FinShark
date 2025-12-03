using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        // these two attributes helps in searching.
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;


        // the below two attributes helps in sorting.
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;

        // the below two attributes helps in pagination.
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20 ;
    }
}