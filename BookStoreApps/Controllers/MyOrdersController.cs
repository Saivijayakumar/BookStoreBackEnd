using Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;

namespace BookStoreApps.Controllers
{
    [Authorize]

    [ApiController]
    [Route("api/[controller]")]
    public class MyOrdersController : ControllerBase
    {
        private readonly IMyOrdersManager manager;

        public MyOrdersController(IMyOrdersManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Order")]
        public IActionResult AddOrder([FromBody] MyOrdersModel orderData)
        {
            try
            {
                var result = this.manager.AddOrder(orderData);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Order Added Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Order Not Added Successfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Orders")]
        public IActionResult GetMyOrders(int userId)
        {
            try
            {
                var result = this.manager.GetMyOrders(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<GetMyOrdersModel>>() { Status = true, Message = "Books Retrived Successfull!!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not Added Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
