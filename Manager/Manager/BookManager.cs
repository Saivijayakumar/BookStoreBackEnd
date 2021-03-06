using Manager.Interface;
using Microsoft.AspNetCore.Http;
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
        public bool AddBook(AddBookModel bookData)
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

        public bool UpdateBook(AddBookModel bookData)
        {
            try
            {
                return this.repository.UpdateBook(bookData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<BookModel> GetBooks()
        {
            try
            {
                return this.repository.GetBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
