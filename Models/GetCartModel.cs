using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GetCartModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public string BookDetail { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }

        public int UserId { get; set; }
        public int CartId { get; set; }

        public int BookCount { get; set; }
        public int TotalCost { get; set; }
    }
}
