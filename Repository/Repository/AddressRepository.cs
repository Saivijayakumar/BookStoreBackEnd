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
    public class AddressRepository :IAddressRepository
    {
        public AddressRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        SqlConnection connection;


        public UserAddress AddAddress(UserAddress userAddress)
        {
            try
            {
                if (userAddress != null)
                {
                    connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("AddAddress", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userAddress.UserId);
                        cmd.Parameters.AddWithValue("@Address", userAddress.Address);
                        cmd.Parameters.AddWithValue("@Type", userAddress.Type);
                        cmd.Parameters.AddWithValue("@City", userAddress.City);
                        cmd.Parameters.AddWithValue("@State", userAddress.State);
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return userAddress;
                        }
                        return null;
                    }
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserAddress> GetAllUserAddress(int userId)
        {
            try
            {
                if (userId != 0)
                {
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("GetAllUserAddress", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();

                        List<UserAddress> userAddresseList = new List<UserAddress>();
                        while (sqlDataReader.Read())
                        {
                            UserAddress userAddress = new UserAddress();
                            userAddress.AddressId = Convert.ToInt32(sqlDataReader["AddressId"]);
                            userAddress.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            userAddress.Address = sqlDataReader["Address"].ToString();
                            userAddress.Type = sqlDataReader["Type"].ToString();
                            userAddress.City = sqlDataReader["City"].ToString();
                            userAddress.State = sqlDataReader["State"].ToString();
                            userAddresseList.Add(userAddress);
                        }
                        if (sqlDataReader.HasRows == false)
                        {
                            throw new Exception("UserId does not Have Address");
                        }
                        return userAddresseList;
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

        public UserAddress UpdateAddress(UserAddress updateData)
        {
            try
            {
                if (updateData != null)
                {
                    connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateAddress", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AddressId", updateData.AddressId);
                        cmd.Parameters.AddWithValue("@Address", updateData.Address);
                        cmd.Parameters.AddWithValue("@Type", updateData.Type);
                        cmd.Parameters.AddWithValue("@City", updateData.City);
                        cmd.Parameters.AddWithValue("@State", updateData.State);
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return updateData;
                        }
                        return null;
                    }
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
