using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IBookManager
    {
        BookModel AddBook(BookModel bookData);
        BookModel UpdateBook(BookModel bookData);
    }
}
