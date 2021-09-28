using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserAddress
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
