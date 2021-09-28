using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ResetPasswordModel
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
