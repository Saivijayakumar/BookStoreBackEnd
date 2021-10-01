using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GetWishListModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public string BookDetail { get; set; }
        public string BookImage { get; set; }
        public string BigImage { get; set; }
        public int BookQuantity { get; set; }
        public int UserId { get; set; }
        public int MyWishListId { get; set; }
    }
}
