using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GetMyOrdersModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string OrderDate { get; set; }
        public int TotalCost { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string BookImage { get; set; }
    }
}
