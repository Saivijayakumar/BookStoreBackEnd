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

        public GetMyOrdersModel AddOrder(MyOrdersModel orderData)
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
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        GetMyOrdersModel getMyOrders = new GetMyOrdersModel();
                        if (sqlDataReader.Read())
                        {
                            
                            getMyOrders.OrderId = Convert.ToInt32(sqlDataReader["OrderId"]);
                        }
                        if (sqlDataReader.HasRows == false)
                        {
                            throw new Exception("Order not placed");
                        }
                        return getMyOrders;
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
        public List<GetMyOrdersModel> GetMyOrders(int userId)
        {
            try
            {
                connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetMyOrders", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("UserId", userId);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    List<GetMyOrdersModel> orderList = new List<GetMyOrdersModel>();

                    while (sqlDataReader.Read())
                    {
                        GetMyOrdersModel getMyOrders = new GetMyOrdersModel();
                        getMyOrders.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        getMyOrders.OrderId = Convert.ToInt32(sqlDataReader["OrderId"]);
                        getMyOrders.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        getMyOrders.Title = sqlDataReader["Title"].ToString();
                        getMyOrders.AuthorName = sqlDataReader["AuthorName"].ToString();
                        getMyOrders.BookImage = sqlDataReader["BookImage"].ToString();
                        getMyOrders.OrderDate = sqlDataReader["OrderDate"].ToString();
                        getMyOrders.TotalCost = Convert.ToInt32(sqlDataReader["TotalCost"]);
                        orderList.Add(getMyOrders);
                    }
                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("No Orders in MyOrder");
                    }
                    return orderList;
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
