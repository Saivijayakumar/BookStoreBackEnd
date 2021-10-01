using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class CartManager :ICartManager
    {
        private readonly ICartRepository repository;

        public CartManager(ICartRepository repository)
        {
            this.repository = repository;
        }

        public bool AddBookToCart(CartModel cartData)
        {
            try
            {
                return this.repository.AddBookToCart(cartData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetCartModel> GetCart(int userId)
        {
            try
            {
                return this.repository.GetCart(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateCountInCart(CartModel cartData)
        {
            try
            {
                return this.repository.UpdateCountInCart(cartData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveBookFromCart(int cartId)
        {
            try
            {
                return this.repository.RemoveBookFromCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
