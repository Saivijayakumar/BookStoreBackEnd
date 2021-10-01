using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Services.Account;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class BookRepository : IBookRepository
    {

        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        SqlConnection connection;

        public bool AddBook(AddBookModel bookData)
        {
            try
            {
                if (bookData != null)
                {
                    BookModel bookModel = new BookModel();
                    var bookImage = AddImage(bookData.BookImage);
                    var bigImage = AddImage(bookData.BigImage);
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[InsertBookData]", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Title", bookData.Title);
                            cmd.Parameters.AddWithValue("@AuthorName", bookData.AuthorName);
                            cmd.Parameters.AddWithValue("@Price", bookData.Price);
                            cmd.Parameters.AddWithValue("@Rating", bookData.Rating);
                            cmd.Parameters.AddWithValue("@BookDetail", bookData.BookDetail);
                            cmd.Parameters.AddWithValue("@BookImage", bookImage);
                        cmd.Parameters.AddWithValue("@BigImage", bigImage);
                        cmd.Parameters.AddWithValue("@BookQuantity", bookData.BookQuantity);
                            int result = cmd.ExecuteNonQuery();
                            if (result != 0)
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                
                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateBook(AddBookModel bookData)
        {
            try
            {
                if (bookData != null)
                {
                    BookModel bookModel = new BookModel();
                    var bookImage = AddImage(bookData.BookImage);
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[UpdateBookData]", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BookId", bookData.BookId);
                            cmd.Parameters.AddWithValue("@Title", bookData.Title);
                            cmd.Parameters.AddWithValue("@AuthorName", bookData.AuthorName);
                            cmd.Parameters.AddWithValue("@Price", bookData.Price);
                            cmd.Parameters.AddWithValue("@Rating", bookData.Rating);
                            cmd.Parameters.AddWithValue("@BookDetail", bookData.BookDetail);
                            cmd.Parameters.AddWithValue("@BookImage", bookImage);
                            cmd.Parameters.AddWithValue("@BookQuantity", bookData.BookQuantity);
                            SqlDataReader sqlDataReader = cmd.ExecuteReader();
                            BookModel book = new BookModel();
                            if (sqlDataReader.Read())
                            {
                                {
                                    book.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                                    book.Title = sqlDataReader["Title"].ToString();
                                    book.AuthorName = sqlDataReader["AuthorName"].ToString();
                                    book.Price = Convert.ToInt32(sqlDataReader["Price"]);
                                    book.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                                    book.BookDetail = sqlDataReader["BookDetail"].ToString();
                                    book.BookImage = sqlDataReader["BookImage"].ToString();
                                    book.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                                };

                            }
                            if (sqlDataReader.HasRows == false)
                            {
                                throw new Exception("BookId does not exist");
                            }
                            return true;
                        }
                    }
                
                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public List<BookModel> GetBooks()
        {
            try
            {
                connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllBooks", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    List<BookModel> bookList = new List<BookModel>();
                    while (sqlDataReader.Read())
                    {
                        BookModel bookModel = new BookModel();
                        bookModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        bookModel.Title = sqlDataReader["Title"].ToString();
                        bookModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                        bookModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                        bookModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        bookModel.BookDetail = sqlDataReader["BookDetail"].ToString();
                        bookModel.BookImage = sqlDataReader["BookImage"].ToString();
                        bookModel.BigImage = sqlDataReader["BigImage"].ToString();
                        bookModel.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                        bookList.Add(bookModel);
                    }
                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("The book database is empty");
                    }
                    return bookList;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private string AddImage(IFormFile image)
        {
            try
            {
                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(Configuration["CloudinaryAccount:CloudName"], Configuration["CloudinaryAccount:ApiKey"], Configuration["CloudinaryAccount:ApiSecret"]);
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream())
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                     var returnImage = uploadResult.Url.ToString();
                    return returnImage;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

      
    }
}
