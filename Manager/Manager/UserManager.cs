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
    }
}
