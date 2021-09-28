
using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApps.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;

        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddBook")]
        public IActionResult AddBook([FromBody] BookModel bookData)
        {
            try
            {
                var result = this.manager.AddBook(bookData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Book Added Successfully!", Data = result });
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

      [HttpPut]
      [Route("api/UpdateBook")]
        public IActionResult UpdateBook([FromBody] BookModel bookData)
        {
            try
            {
                var result = this.manager.UpdateBook(bookData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Book Updated Successfully!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not Updated Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
