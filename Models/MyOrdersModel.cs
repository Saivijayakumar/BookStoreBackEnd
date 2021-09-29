using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class MyOrdersModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int AddressId { get; set; }
        public string OrderDate { get; set; }
        public int TotalCost { get; set; }
    }
}
