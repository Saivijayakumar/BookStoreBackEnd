using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class OTPModel
    {
        public int UserId { get; set; }
        public string OTP { get; set; }

        public string EmailId { get; set; }
    }
}
