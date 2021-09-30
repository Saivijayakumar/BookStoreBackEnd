using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IBookRepository
    {
        bool AddBook(AddBookModel bookData);
        bool UpdateBook(AddBookModel bookData);
        List<BookModel> GetBooks();

    }
}
