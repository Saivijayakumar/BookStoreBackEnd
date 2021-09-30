
using Manager.Interface;
using Microsoft.AspNetCore.Http;
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
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;

        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Book")]
        public IActionResult AddBook([FromForm] AddBookModel bookData)
        {
            try
            {
                var result = this.manager.AddBook(bookData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Added Successfully!"});
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
      [Route("Book")]
        public IActionResult UpdateBook([FromForm] AddBookModel bookData)
        {
            try
            {
                var result = this.manager.UpdateBook(bookData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Book Updated Successfully!"});
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
        [HttpGet]
        [Route("Books")]
        public IActionResult GetBooks()
        {
            try
            {
                var result = this.manager.GetBooks();
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<BookModel>>() { Status = true, Message = "Books Retrived Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Books Retrived Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
