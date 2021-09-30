using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IAddressRepository
    {
        UserAddress AddAddress(UserAddress userAddress);
        public List<UserAddress> GetAllUserAddress(int userId);
        UserAddress UpdateAddress(UserAddress updateData);
    }
}
