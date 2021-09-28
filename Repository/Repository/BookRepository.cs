using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class BookRepository :IBookRepository
    {
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True";
        SqlConnection connection = new SqlConnection(ConnectionString);
        public BookModel AddBook(BookModel bookData)
        {
            try
            {
                if (bookData != null)
                {
                    BookModel bookModel = new BookModel();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
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
                            cmd.Parameters.AddWithValue("@BookImage", bookData.BookImage);
                            cmd.Parameters.AddWithValue("@BookQuantity", bookData.BookQuantity);
                            int result = cmd.ExecuteNonQuery();
                            if (result != 0)
                            {
                                return bookData;
                            }
                            return null;
                        }
                    }
                }
                return null;
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
    }
}
