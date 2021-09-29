using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApps.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;

        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/AddBookToCart")]
        public IActionResult AddBookToCart([FromBody] CartModel cartData)
        {
            try
            {
                var result = this.manager.AddBookToCart(cartData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Added To Cart Successfully!" });
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

        [HttpGet]
        [Route("api/GetCart")]
        public IActionResult GetCart(int userId)
        {
            try
            {
                var result = this.manager.GetCart(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<GetCartModel>>() { Status = true, Message = "Books Retrived Successfull!!", Data=result });
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
