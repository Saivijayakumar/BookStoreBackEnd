using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class AddressManager :IAddressManager
    {
        private readonly IAddressRepository repository;

        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
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

    }
}
