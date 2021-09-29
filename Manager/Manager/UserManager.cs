using Manager.Interface;
using Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public RegisterModel Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                return this.repository.Login(loginData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public OTPModel ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ResetPassword(ResetPasswordModel resetData)
        {
            try
            {
                return this.repository.ResetPassword(resetData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserAddress AddAddress(UserAddress userAddress)
        {
            try
            {
                return this.repository.AddAddress(userAddress);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<UserAddress> GetAllUserAddress(int userId)
        {
            try
            {
                return this.repository.GetAllUserAddress(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public UserAddress UpdateAddress(UserAddress updateData)
        {
            try
            {
                return this.repository.UpdateAddress(updateData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditPersonalDetails(RegisterModel userData)
        {
            try
            {
                return this.repository.EditPersonalDetails(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
