using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApps.Controllers
{
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
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Order Added Successfully!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Order Not Added Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
