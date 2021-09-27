using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public interface IUserRepository
    {
        public RegisterModel Register(RegisterModel userData);
    }
}
