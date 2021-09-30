using Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApps.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;

        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("Address")]
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
        [Route("Address")]
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
        [Route("Address")]
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
    }
}
