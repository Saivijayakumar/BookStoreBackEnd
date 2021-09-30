using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IBookManager
    {
        bool AddBook(AddBookModel bookData);
        bool UpdateBook(AddBookModel bookData);
        List<BookModel> GetBooks();      
    }
}
