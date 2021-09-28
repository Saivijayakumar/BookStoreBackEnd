using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class BookManager :IBookManager
    {
        private readonly IBookRepository repository;

        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }
        public BookModel AddBook(BookModel bookData)
        {
            try
            {
                return this.repository.AddBook(bookData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
