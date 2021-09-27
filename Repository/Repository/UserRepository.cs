using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration Configuration { get; }
        public static string ConnectionString { get; private set; }

        public UserRepository(IConfiguration configuration)
        {

            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:DataSource"];


        }

        public RegisterModel Register(RegisterModel userData)
        {
            try
            {
                
                if (userData != null)
                {
                    RegisterModel registerModel = new RegisterModel();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (connection)
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("Registration", connection);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UserName", userData.UserName);
                            cmd.Parameters.AddWithValue("@EmailId", userData.EmailId);
                            cmd.Parameters.AddWithValue("@Password", EncryptPassWord(userData.Password));

                            SqlDataReader sqlDataReader = cmd.ExecuteReader();
                            if (sqlDataReader.HasRows)
                            {
                                if (sqlDataReader.Read())
                                {
                                    registerModel.UserName = sqlDataReader["UserName"].ToString();
                                    registerModel.EmailId = sqlDataReader["EmailId"].ToString();
                                }
                                sqlDataReader.Close();
                            }
                            connection.Close();
                            return registerModel;

                        }                      

                    }                
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EncryptPassWord(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordInBytes);
            return encodePassword;
        }
    }
}
