using Microsoft.Extensions.Configuration;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class MyWishListRepository : IMyWishListRepository
    {

        public MyWishListRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        SqlConnection connection;
        public bool AddBookToMyWishList(MyWishListModel myWishList)
        {
            try
            {
                if (myWishList != null)
                {
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[AddBookToMyWishList]", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", myWishList.UserId);
                        cmd.Parameters.AddWithValue("@BookId", myWishList.BookId);
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
        public List<BookModel> GetBookFromMyWishList(int userId)
        {
            try
            {
                connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetBookFromMyWishList]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("UserId", userId);
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
                        bookModel.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                        bookList.Add(bookModel);
                    }
                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("No Books in MyWishList");
                    }
                    return bookList;
                }
            }
            catch (Exception ex)
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
