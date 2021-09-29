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
    public class CartRepository : ICartRepository
    {
        public CartRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        SqlConnection connection;
        public bool AddBookToCart(CartModel cartData)
        {
            try
            {
                if (cartData != null)
                {
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[AddBookToCart]", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", cartData.UserId);
                        cmd.Parameters.AddWithValue("@BookId", cartData.BookId);
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
        public List<GetCartModel> GetCart(int userId)
        {
            try
            {
                connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetCart]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("UserId", userId);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    List<GetCartModel> list = new List<GetCartModel>();

                    while (sqlDataReader.Read())
                    {
                        GetCartModel getCartModel = new GetCartModel();
                        getCartModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        getCartModel.Title = sqlDataReader["Title"].ToString();
                        getCartModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                        getCartModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                        getCartModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        getCartModel.BookDetail = sqlDataReader["BookDetail"].ToString();
                        getCartModel.BookImage = sqlDataReader["BookImage"].ToString();
                        getCartModel.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                        getCartModel.CartId = Convert.ToInt32(sqlDataReader["CartId"]);
                        getCartModel.BookCount = Convert.ToInt32(sqlDataReader["BookCount"]);
                        getCartModel.TotalCost = Convert.ToInt32(sqlDataReader["TotalCost"]);
                        getCartModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        list.Add(getCartModel);
                    }
                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("No Books in Cart list");
                    }
                    return list;
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
