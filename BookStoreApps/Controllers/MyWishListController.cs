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
 

    [ApiController]
    [Route("api/[controller]")]
    public class MyWishListController : ControllerBase
    {
        private readonly IMyWishListManager manager;

        public MyWishListController(IMyWishListManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("WishList")]
        public IActionResult AddBookToMyWishList([FromBody] MyWishListModel myWishList)
        {
            try
            {
                var result = this.manager.AddBookToMyWishList(myWishList);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Added To myWishList Successfully!" });
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
        [Route("api/Book")]
        public IActionResult GetBookFromMyWishList(int userId)
        {
            try
            {
                var result = this.manager.GetBookFromMyWishList(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<GetWishListModel>>() { Status = true, Message = "MyWishList Books Retrived Successfully!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "MyWishList Books Retrived UnSuccessfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("api/Book")]
        public IActionResult RemoveBookFromMyWishList(int myWishListId)
        {
            try
            {
                var result = this.manager.RemoveBookFromMyWishList(myWishListId);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Removed From myWishList Successfully!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Remove Book From myWishList UnSuccessfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

    }
}
