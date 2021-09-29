using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        SqlConnection connection;
        public RegisterModel Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    RegisterModel registerModel = new RegisterModel();
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                     using (connection)
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("Registration", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FullName", userData.FullName);
                            cmd.Parameters.AddWithValue("@EmailId", userData.EmailId);
                            cmd.Parameters.AddWithValue("@Password", EncryptPassWord(userData.Password));
                            cmd.Parameters.AddWithValue("@MobileNumber", userData.MobileNumber);
                            int result = cmd.ExecuteNonQuery();
                         
                            if (result != 0)
                            {
                                return userData;
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
            finally
            {
                connection.Close();
            }
        }
        public string EncryptPassWord(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordInBytes);
            return encodePassword;
        }
        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                if (loginData != null)
                {
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("Login", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@EmailId", loginData.EmailId);
                        cmd.Parameters.AddWithValue("@Password", EncryptPassWord(loginData.Password));
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();

                        RegisterModel registerModel = new RegisterModel();
                        if (sqlDataReader.Read())
                        {
                            registerModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            registerModel.FullName = sqlDataReader["FullName"].ToString();
                            registerModel.EmailId = sqlDataReader["EmailId"].ToString();
                            registerModel.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                            registerModel.Password = sqlDataReader["Password"].ToString();
                        }
                        if (sqlDataReader.HasRows == false)
                        {
                            throw new Exception("EmailId does not exist");
                        }
                        else if (registerModel.Password != EncryptPassWord(loginData.Password))
                        {
                            throw new Exception("Password does not match");
                        }
                        registerModel.Password = null;
                        return registerModel;
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

        public OTPModel ForgotPassword(string email)
        {
            try
            {
                if (email != null)
                {
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[ForgotPassword]", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@EmailId", email);
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        OTPModel otpModel = new OTPModel();
                        if (sqlDataReader.Read())
                        {
                            otpModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            otpModel.EmailId = sqlDataReader["EmailId"].ToString();
                        }
                        var generatedOTP = GenerateRandomOTP();
                        this.SendMSMQ(generatedOTP);
                        if (this.SendMail(email))
                        {
                            otpModel.OTP = generatedOTP;
                            return otpModel;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private string GenerateRandomOTP()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string OTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            int length = 4;
            for (int i = 0; i < length; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                OTP += sTempChars;
            }
            return OTP;
        }


        private void SendMSMQ(string otp)
        {
            MessageQueue msgqueue;

            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msgqueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            var formatter = new BinaryMessageFormatter();
            message.Formatter = formatter;
            msgqueue.Label = "url Link";
            message.Body = "OTP for Reset Password "+otp;
            msgqueue.Send(message);
        }

        private bool SendMail(string email)
        {
            string emailMessage = this.ReceiveMSMQ();
            if (this.SendMailToUser(email, emailMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string ReceiveMSMQ()
        {
            // for reading msmq
            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            string emailMessage = receivemsg.Body.ToString();
            return emailMessage;
        }

        private bool SendMailToUser(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            mailMessage.From = new MailAddress("radhika.shankar1220@gmail.com");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Link to reset your password for BookStore App";
            mailMessage.Body = message;
            smtp.EnableSsl = true;
            mailMessage.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("radhika.shankar1220@gmail.com", "kriyanthi");
            smtp.Send(mailMessage);
            return true;
        }
        public bool ResetPassword(ResetPasswordModel resetData)
        {
            try
            {
                if (resetData != null)
                {
                    string newPassword = EncryptPassWord(resetData.Password);
                    connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (connection)
                    { 
                        connection.Open();
                            SqlCommand cmd = new SqlCommand("RestPassword", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserId", resetData.UserId);
                            cmd.Parameters.AddWithValue("@Password", newPassword);
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
        }

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

        public bool EditPersonalDetails(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (connection)
                    { 
                        connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[EditPersonalDetails]", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserId", userData.UserId);
                            cmd.Parameters.AddWithValue("@FullName", userData.FullName);
                            cmd.Parameters.AddWithValue("@EmailId", userData.EmailId);
                            cmd.Parameters.AddWithValue("@Password", EncryptPassWord(userData.Password));
                            cmd.Parameters.AddWithValue("@MobileNumber", userData.MobileNumber);
                            SqlDataReader sqlDataReader = cmd.ExecuteReader();
                            RegisterModel registerModel = new RegisterModel();
                            if (sqlDataReader.HasRows == false)
                            {
                                throw new Exception("UserId does not exist");
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
    }
}