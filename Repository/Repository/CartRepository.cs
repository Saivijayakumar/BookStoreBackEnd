using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class CartRepository :ICartRepository
    {
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True";
        SqlConnection connection = new SqlConnection(ConnectionString);
        public bool AddBookToCart(CartModel cartData)
        {
            try
            {
                if (cartData != null)
                {
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
    }
}
