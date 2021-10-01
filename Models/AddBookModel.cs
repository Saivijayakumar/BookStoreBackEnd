using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AddBookModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public string BookDetail { get; set; }
        public IFormFile BookImage { get; set; }
        public IFormFile BigImage { get; set; }
        public int BookQuantity { get; set; }
    }
}
