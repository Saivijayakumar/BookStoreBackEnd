using Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    
    public class UserController : ControllerBase
    {

        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterModel userData)
        {
            try
            {
                var result = this.manager.Register(userData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Registration Successfull!", Data= result});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
