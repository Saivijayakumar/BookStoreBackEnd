using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public interface IUserRepository
    {
        public RegisterModel Register(RegisterModel userData);
        RegisterModel Login(LoginModel loginData);

        OTPModel ForgotPassword(string email);
        bool ResetPassword(ResetPasswordModel resetData);


        bool EditPersonalDetails(RegisterModel userData);
    }
}
