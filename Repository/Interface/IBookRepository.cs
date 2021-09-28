using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IBookRepository
    {
        BookModel AddBook(BookModel bookData);
        BookModel UpdateBook(BookModel bookData);
    }
}
