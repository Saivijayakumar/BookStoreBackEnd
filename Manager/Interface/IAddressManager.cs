using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IAddressManager
    {
        UserAddress AddAddress(UserAddress userAddress);

        public List<UserAddress> GetAllUserAddress(int userId);
        UserAddress UpdateAddress(UserAddress updateData);
    }
}
