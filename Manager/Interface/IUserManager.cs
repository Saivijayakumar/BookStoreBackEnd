using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IUserManager
    {
        RegisterModel Register(RegisterModel userData);

        RegisterModel Login(LoginModel loginData);
        OTPModel ForgotPassword(string email);
        bool ResetPassword(ResetPasswordModel resetData);
        UserAddress AddAddress(UserAddress userAddress);
    }
}
