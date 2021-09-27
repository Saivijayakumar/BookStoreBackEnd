using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class RegisterModel
    {
         
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
