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
    public class MyOrderRepository : IMyOrderRepository
    {
        public MyOrderRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        SqlConnection connection;

        public bool AddOrder(MyOrdersModel orderData)
        {
            try
            {
                if (orderData != null)
                {
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("AddOrder", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", orderData.UserId);
                        cmd.Parameters.AddWithValue("@BookId", orderData.BookId);
                        cmd.Parameters.AddWithValue("@AddressId", orderData.AddressId);
                        cmd.Parameters.AddWithValue("@OrderDate", orderData.OrderDate);
                        cmd.Parameters.AddWithValue("@TotalCost", orderData.TotalCost);
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
