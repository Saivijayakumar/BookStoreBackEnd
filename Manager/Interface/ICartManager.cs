using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface ICartManager
    {
        bool AddBookToCart(CartModel cartData);

        public List<GetCartModel> GetCart(int userId);

        bool UpdateCountInCart(CartModel cartData);
        bool RemoveBookFromCart(int cartId);
    }
}
