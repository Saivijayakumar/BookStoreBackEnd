﻿using Manager.Interface;
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
        [Route("api/register)")]
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
        [HttpPost]
        [Route("api/Login)")]
        public IActionResult Login([FromBody] LoginModel loginData)
        {
            try
            {
                var result = this.manager.Login(loginData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Login Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/forgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = this.manager.ForgotPassword(email);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<OTPModel>() { Status = true, Message = "Please check your email",Data=result});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Email not Sent" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel resetData)
        {
            try
            {
                var result = this.manager.ResetPassword(resetData);
                if (result)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Reset Successfull!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Reset Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/AddAddress")]
        public IActionResult AddAddress([FromBody] UserAddress userAddress)
        {
            try
            {
                var result = this.manager.AddAddress(userAddress);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserAddress>() { Status = true, Message = "Address Added Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Adding Address Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetAddress")]
        public IActionResult GetAllUserAddress(int userId)
        {
            try
            {
                var result = this.manager.GetAllUserAddress(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<UserAddress>>() { Status = true, Message = "Address Retrived Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Address Retrived Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/EditAddress")]
        public IActionResult UpdateAddress([FromBody] UserAddress updateData)
        {
            try
            {
                var result = this.manager.UpdateAddress(updateData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserAddress>() { Status = true, Message = "Update Address Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Update Address Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/EditPersonalDetails")]
        public IActionResult EditPersonalDetails([FromBody] RegisterModel userData)
        {
            try
            {
                var result = this.manager.EditPersonalDetails(userData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Update Personal Details Successfull!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Update Personal Details Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
